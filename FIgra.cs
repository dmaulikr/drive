using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;

namespace _drive
{
    public partial class FIgra : Form
    {
        //Instance Variables
        #region
        double lastTime, thisTime, diff;
        Sprite[] sprites = new Sprite[1000];
        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        int spriteCount = 0, soundCount = 0;
        string inkey;
        int mouseKey, mouseXp, mouseYp;
        Rectangle Collision, Collision2;
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        #endregion

        private int bodovi;
        public int Bodovi
        {
            get { return bodovi; }
            set { bodovi = value; }
        }

        int level, ubrzanje;
        bool prekid;
        bool[] promet;
        int[] trake;
        Random r1 = new Random();
        Random r2 = new Random();

        //Structs
        #region 
        public struct Sprite
        {
            public string image;
            public Bitmap bmp;
            public int x, y, width, height;
            public bool show;

            public Sprite(string images, int p1, int p2)
            {
                bmp = new Bitmap(images);
                image = images;
                x = p1;
                y = p2;
                width = bmp.Width;
                height = bmp.Height;
                show = true;
            }

            public Sprite(string images, int p1, int p2, int w, int h)
            {
                bmp = new Bitmap(images);
                image = images;
                x = p1;
                y = p2;
                width = w;
                height = h;
                show = true;
            }
        }

        #endregion

        public FIgra()
        {
            InitializeComponent();
        }

        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;

            //inicijalizacija GUI-a
            setTitle("DR!VE");
            setBackgroundColour(Color.GreenYellow);
            loadSprite("cesta.png", 0, 65, -1500);
            panel1.ForeColor = Color.Black;
            panel1.BackColor = Color.GreenYellow;

            //inicijalizacija igraca i levela
            loadSprite("autoIgrac.png", 5, 475, 275);
            this.Bodovi = 0;
            level = 1;
            prekid = false;
            promet = new bool[5] { false, false, false, false, false }; //sadrže li trake aute? 
            trake = new int[10] { 350, 475, 475, 350, 350, 475, 350, 475, 350, 475 }; //randomizacija učitavanja protivnika 
        }

        private void Update(object sender, EventArgs e)
        {   
            Okruzenje();
            Kontrole();
            Rezultat();
            Kolizije();
            this.Refresh();
        }

        private void Okruzenje()
        {
            //početni položaj po Y za protivnike, kvazirandom
            //ovo služi da nas igra ne sudari sama
            //uvijek mora postojati dovoljno mjesta da igrač pretekne protivnika
            int pocetniY1 = r1.Next(200, 400) + r2.Next(200, 300);
            int pocetniY2 = pocetniY1 + r1.Next(200, 300);

            //učitavanje ceste
            if (!promet[4])
            {
                loadSprite("cesta.png", 0, 65, -1500);
                promet[4] = true;
            }

            //učitavanje auta po trakama
            if (!promet[3])
            {
                loadSprite("autoProtivnik.png", 1, trake[r1.Next(0, 10)], spriteY(2) - pocetniY1);
                promet[3] = true;
            }

            if (!promet[2])
            {
                loadSprite("autoProtivnik.png", 2, trake[r2.Next(0, 10)], spriteY(1) - pocetniY2);
                promet[2] = true;
            }

            if (!promet[1])
            {
                loadSprite("autoStatist.png", 3, 210, r1.Next(-500, -200));
                promet[1] = true;
            }

            if (!promet[0])
            {
                loadSprite("autoStatist.png", 4, 80, r1.Next(-500, -200));
                promet[0] = true;
            }

            if (spriteY(0) > -250)
                promet[4] = false;

            if (spriteY(1) > 500 && !prekid)
                promet[3] = false;

            if (spriteY(2) > 500 && !prekid)
                promet[2] = false;

            if (spriteY(3) > 500)
               promet[1] = false;

            if (spriteY(4) > 500)
                promet[0] = false;

            //auti i cesta padaju put dolje pa se dobiva privid vožnje
            if (!prekid)
            {
                for (int i = 0; i < 5; i++)
                    moveSprite(i, spriteX(i), spriteY(i) + level + 5 + ubrzanje);
            }

            //bodovi i level se ispisuju na labele
            lblBodovi.Text = this.Bodovi.ToString();
            lblLevel.Text = level.ToString();

            //forma se zatvara nakon igre
            if (isKeyPressed(Keys.Space) && prekid)
                this.Close();
        }

        private void Kontrole()
        {
            if (!prekid)
            {
                switch (inkey)
                {
                    case "Up":
                        picUbrzanje.Show(); //pokaži indikator
                        moveSprite(5, spriteX(5), 280);
                        ubrzanje = 5;
                        break;

                    case "Down":
                        picUbrzanje.Hide();
                        moveSprite(5, spriteX(5), 270);
                        ubrzanje = 0;
                        break;

                    case "Left":
                        moveSprite(5, spriteX(5) - level - 3 - ubrzanje, spriteY(5) + ubrzanje);
                        changeSpriteImage(5, "autoIgracLijevo.png");
                        moveSprite(5, spriteX(5), 280);
                        break;

                    case "Right":
                        moveSprite(5, spriteX(5) + level + 3 + ubrzanje, spriteY(5) + ubrzanje);
                        changeSpriteImage(5, "autoIgracDesno.png");
                        moveSprite(5, spriteX(5), 280);
                        break;

                    default:
                        changeSpriteImage(5, "autoIgrac.png");
                        moveSprite(5, spriteX(5), 275);
                        break;
                }
            }
        }

        private void Rezultat()
        {
            if ((spriteY(1) > 500 && promet[3]) || (spriteY(2) > 500 && promet[2]) && !prekid)
            {
                this.Bodovi++;
                //igra se ubrzava prelaskom parnog protivnika
                //namjerno upetljano da se uspori update, inače leveli lete
                if (this.Bodovi % 2 == 0 && this.Bodovi > 0)
                    level++;  
            }
        }

        private void Kolizije()
        {
            //sudar
            if(spriteCollision(5, 1, 2)) //preopteretio sam ugrađenu metodu
            {
                prekid = true;
                moveSprite(5, spriteX(5), spriteY(5) + 10 + 4 * ubrzanje);
                moveSprite(1, spriteX(1), spriteY(1) - 10 - 4 * ubrzanje);
                moveSprite(2, spriteX(2), spriteY(2) - 10 - 4 * ubrzanje);
                loadSprite("sudar.png", 6);
            }

            //prometni prekrsaj, izlijetanje s ceste ili ulazak u suprotni smijer
            if(spriteX(5) < 320 || spriteX(5) + spriteWidth(5) > 580 && spriteVisible(5))
            {
                prekid = true;
                loadSprite("prekrsaj.png", 6);
            }
        }

        // Start of Game Methods
        #region

        //This is the beginning of the setter methods



        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }


        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }
              
        }

        public void setTitle(string title)
        {
            this.Text = title;
        }

        public void setBackgroundColour(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        public void setBackgroundColour(Color col)
        {
            this.BackColor = col;
        }

        public void setBackgroundImage(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        public void setBackgroundImageLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }
        
        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        public void loadSprite(string file, int spriteNum)
        {
            spriteCount++;
            sprites[spriteNum] = new Sprite(file, 0, 0);
        }

        public void loadSprite(string file, int spriteNum, int x, int y)
        {
            spriteCount++;
            sprites[spriteNum] = new Sprite(file, x, y);
        }

        public void loadSprite(string file, int spriteNum, int x, int y, int w, int h)
        {
            spriteCount++;
            sprites[spriteNum] = new Sprite(file, x, y, w, h);
        }

        public void rotateSprite(int spriteNum, int angle)
        {
            if (angle == 90)
                sprites[spriteNum].bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (angle == 180)
                sprites[spriteNum].bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            if (angle == 270)
                sprites[spriteNum].bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        public void scaleSprite(int spriteNum, int scale)
        {
            float sx = float.Parse(sprites[spriteNum].width.ToString());
            float sy = float.Parse(sprites[spriteNum].height.ToString());
            float nsx = ((sx / 100) * scale); 
            float nsy = ((sy / 100) * scale);

            sprites[spriteNum].width = Convert.ToInt32(nsx);
            sprites[spriteNum].height = Convert.ToInt32(nsy);
        }

        public void moveSprite(int spriteNum, int x, int y)
        {
            sprites[spriteNum].x = x;
            sprites[spriteNum].y = y;
        }

        public void setImageColorKey(int spriteNum, int r, int g, int b)
        {
            sprites[spriteNum].bmp.MakeTransparent(Color.FromArgb(r, g, b));
        }

        public void setImageColorKey(int spriteNum, Color col)
        {
            sprites[spriteNum].bmp.MakeTransparent(col);
        }

        public void setSpriteVisible(int spriteNum, bool ans)
        {
            sprites[spriteNum].show = ans;
        }

        public void hideSprite(int spriteNum)
        {
            sprites[spriteNum].show = false;
        }


        public void flipSprite(int spriteNum, string fliptype)
        {
            if(fliptype.ToLower() == "none")
            sprites[spriteNum].bmp.RotateFlip(RotateFlipType.RotateNoneFlipNone);

            if (fliptype.ToLower() == "horizontal")
            sprites[spriteNum].bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            if (fliptype.ToLower() == "horizontalvertical")
            sprites[spriteNum].bmp.RotateFlip(RotateFlipType.RotateNoneFlipXY);

            if (fliptype.ToLower() == "vertical")
            sprites[spriteNum].bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        public void changeSpriteImage(int spriteNum, string file)
        {
            sprites[spriteNum] = new Sprite(file, sprites[spriteNum].x, sprites[spriteNum].y);
        }

        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        public void hideMouse()
        {
            Cursor.Hide();
        }

        public void showMouse()
        {
            Cursor.Show();
        }



        //This is the beginning of the getter methods

        public bool spriteExist(int spriteNum)
        {
            if (sprites[spriteNum].bmp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int spriteX(int spriteNum)
        {
            return sprites[spriteNum].x;
        }

        public int spriteY(int spriteNum)
        {
            return sprites[spriteNum].y;
        }

        public int spriteWidth(int spriteNum)
        {
            return sprites[spriteNum].width;
        }

        public int spriteHeight(int spriteNum)
        {
            return sprites[spriteNum].height;
        }

        public bool spriteVisible(int spriteNum)
        {
            return sprites[spriteNum].show;
        }

        public string spriteImage(int spriteNum)
        {
            return sprites[spriteNum].bmp.ToString();
        }

        public bool isKeyPressed(string key)
        {
            if (inkey == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isKeyPressed(Keys key)
        {
            if (inkey == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool spriteCollision(int spriteNum1, int spriteNum2)
        {
            Rectangle sp1 = new Rectangle(sprites[spriteNum1].x, sprites[spriteNum1].y, sprites[spriteNum1].width, sprites[spriteNum1].height);
            Rectangle sp2 = new Rectangle(sprites[spriteNum2].x, sprites[spriteNum2].y, sprites[spriteNum2].width, sprites[spriteNum2].height);
            Collision = Rectangle.Intersect(sp1, sp2);

            if (!Collision.IsEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool spriteCollision(int spriteNum1, int spriteNum2, int spriteNum3)
        {
            Rectangle sp1 = new Rectangle(sprites[spriteNum1].x + 6, sprites[spriteNum1].y, sprites[spriteNum1].width - 6, sprites[spriteNum1].height - 5);
            Rectangle sp2 = new Rectangle(sprites[spriteNum2].x + 6, sprites[spriteNum2].y, sprites[spriteNum2].width - 6, sprites[spriteNum2].height - 5);
            Rectangle sp3 = new Rectangle(sprites[spriteNum3].x + 6, sprites[spriteNum3].y, sprites[spriteNum3].width - 6, sprites[spriteNum3].height - 5);

            Collision = Rectangle.Intersect(sp1, sp2);
            Collision2 = Rectangle.Intersect(sp1, sp3);
            

            if ((!Collision.IsEmpty || !Collision2.IsEmpty))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        public bool isMousePressed() {
            if (mouseKey == 1) return true;
            else return false;
        }

        public int mouseX()
        {
            return mouseXp;
        }

        public int mouseY()
        {
            return mouseYp;
        }

        #endregion


        //Game Update and Input
        #region
        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Sprite sprite in sprites)
            {
                if (sprite.bmp != null && sprite.show == true)
                    g.DrawImage(sprite.bmp, new Rectangle(sprite.x, sprite.y, sprite.width, sprite.height));
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            inkey = e.KeyCode.ToString();
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            inkey = "";
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            mouseKey = 1;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            mouseKey = 1;
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            mouseKey = 0;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseXp = e.X;
            mouseYp = e.Y;
        }

#endregion
    }
}
