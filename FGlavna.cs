using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _drive
{
    public partial class FGlavna : Form, IStanje
    {
        public FGlavna()
        {
            InitializeComponent();
        }


        List<Igrac> lista = new List<Igrac>();

        public void Citaj()
        {
            if (File.Exists("highscores.txt"))
            {
                using (StreamReader sr = new StreamReader(File.OpenRead("highscores.txt"))) //čita podatke iz nje i puni listu
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        int bodovi = int.Parse(s.Split('$')[0]);
                        string ime = s.Split('$')[1];
                        DateTime datum = DateTime.Parse(s.Split('$')[2]);
                        lista.Add(new Igrac(bodovi, ime, datum));
                    }
                }
            }
        }

        public void Pisi()
        {
            using (StreamWriter sw = File.CreateText("highscores.txt"))
            {
                foreach (Igrac x in lista)
                    sw.WriteLine(String.Format("{0}${1}${2}", x.Bodovi, x.Ime, x.Datum));
            }  
        }

        public void Osvjezi()
        {
            lista.Sort(); //sortiraj
            lstLista.Items.Clear(); //ponovo popuni listbox
            foreach (Igrac x in lista)
                lstLista.Items.Add(x);

            for (int i = 0; i < lista.Count; i++)
                lstLista.SetSelected(i, false); //odznači sve
            DateTime d = DateTime.Now.AddYears(-100); //danas - 100 godina. šta je sigurno, sigurno je
            for (int i = 0; i < lista.Count; i++)
                if (lista[i].Datum.CompareTo(d) == 1)
                {
                    d = lista[i].Datum;
                    lstLista.SetSelected(i, true); //označi novijeg
                }
        }

        public void Brisi()
        {
            lstLista.Items.Clear(); //briši iz listboxa
            File.Delete("highscores.txt"); //briši datoteku
            lista.Clear(); //briši iz liste
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstLista.Hide();
            btnBrisi.Hide();
            Citaj();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //početak igre
            FIgra igra = new FIgra();
            igra.ShowDialog();

            //nakon igranja prikaži formu za upis imena
            FUpis frm2 = new FUpis();
            frm2.ShowDialog();

            //nakon toga objekt dobiva vrijednosti svih svojih svojstava, instancira se i sprema u listu
            Igrac igr = new Igrac(igra.Bodovi, frm2.Ime, DateTime.Now);
            lista.Add(igr);

            //losta se osvježava
            Osvjezi();
            Pisi();
            lstLista.Show();
            btnBrisi.Show();
        }

        private void btnPravila_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click START and overtake the cars. Each overtake brings 1 point. The game gradually speeds up. The goal is to stay in traffic as long as possible, avoiding crashes and traffic violations. But don't worry, you won't get a speeding ticket. Crashes and driving in the opposite direction end the game. Stick to your side. \n\nControls: \nSteering: LEFT, RIGHT \nSpeed Boost On: UP \nSpeed Boost Off: DOWN \n\nAbout: \nMy take on the classic arcade racing game. \nMade for a college project, written in C# using Microsoft Visual Studio and the Game SDK engine. \nAuthor: Leo Hajder (github.com/lhajder)", "DR!VE - Rules & Controls");
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            Osvjezi();
            lstLista.Show();
            btnBrisi.Show();
        }

        private void btnBrisi_Click(object sender, EventArgs e)
        {
            Brisi();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstLista_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt1 = new ToolTip();
            tt1.SetToolTip(this.lstLista, "The newest score is highlighted.");
        }
    }
}