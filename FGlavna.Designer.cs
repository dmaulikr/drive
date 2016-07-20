namespace _drive
{
    partial class FGlavna
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FGlavna));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnLista = new System.Windows.Forms.Button();
            this.btnBrisi = new System.Windows.Forms.Button();
            this.lstLista = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPravila = new System.Windows.Forms.Button();
            this.tt1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Location = new System.Drawing.Point(12, 227);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(75, 23);
            this.btnIzlaz.TabIndex = 1;
            this.btnIzlaz.Text = "Exit";
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnLista
            // 
            this.btnLista.Location = new System.Drawing.Point(240, 12);
            this.btnLista.Name = "btnLista";
            this.btnLista.Size = new System.Drawing.Size(75, 23);
            this.btnLista.TabIndex = 2;
            this.btnLista.Text = "Highscores";
            this.btnLista.UseVisualStyleBackColor = true;
            this.btnLista.Click += new System.EventHandler(this.btnLista_Click);
            // 
            // btnBrisi
            // 
            this.btnBrisi.Location = new System.Drawing.Point(240, 227);
            this.btnBrisi.Name = "btnBrisi";
            this.btnBrisi.Size = new System.Drawing.Size(75, 23);
            this.btnBrisi.TabIndex = 3;
            this.btnBrisi.Text = "Reset";
            this.btnBrisi.UseVisualStyleBackColor = true;
            this.btnBrisi.Click += new System.EventHandler(this.btnBrisi_Click);
            // 
            // lstLista
            // 
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(152, 41);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(163, 173);
            this.lstLista.TabIndex = 4;
            this.lstLista.MouseHover += new System.EventHandler(this.lstLista_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 173);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnPravila
            // 
            this.btnPravila.Location = new System.Drawing.Point(125, 12);
            this.btnPravila.Name = "btnPravila";
            this.btnPravila.Size = new System.Drawing.Size(75, 23);
            this.btnPravila.TabIndex = 6;
            this.btnPravila.Text = "Controls";
            this.btnPravila.UseVisualStyleBackColor = true;
            this.btnPravila.Click += new System.EventHandler(this.btnPravila_Click);
            // 
            // FGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 262);
            this.Controls.Add(this.btnPravila);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lstLista);
            this.Controls.Add(this.btnBrisi);
            this.Controls.Add(this.btnLista);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnStart);
            this.Name = "FGlavna";
            this.Text = "DR!VE";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnLista;
        private System.Windows.Forms.Button btnBrisi;
        private System.Windows.Forms.ListBox lstLista;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPravila;
        private System.Windows.Forms.ToolTip tt1;
    }
}