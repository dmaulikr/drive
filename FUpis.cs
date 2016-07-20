using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _drive
{
    public partial class FUpis : Form
    {
        public FUpis()
        {
            InitializeComponent();
        }

        private string ime;
        public string Ime
        {
            get { return ime; }
            set { ime = value; }       
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //upis imena i provjera unosa
                if (txtIme.Text == "")
                    throw new ArgumentException("Please write your name.");
                else
                    this.Ime = txtIme.Text;
            }

            catch (ArgumentException a)
            {
                MessageBox.Show(a.Message);
                return;
            }

            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblIspis.Text = "Write your name and check out the highscores.";
            txtIme.Focus();
        }

        private void txtIme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk.PerformClick();
        }

        private void FUpis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtIme.Text == "")
                this.Ime = "John Doe";
        }   
    }
}
