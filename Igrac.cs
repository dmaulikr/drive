using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _drive
{
    public class Igrac : IComparable
    {
        private string ime;
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        private int bodovi;
        public int Bodovi
        {
            get { return bodovi; }
            set { bodovi = value; }
        }

        private DateTime datum;
        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public Igrac()
        {
            this.Ime = "Igrač";
            this.Bodovi = 0;
            this.Datum = DateTime.Now;
        }

        public Igrac(int b, string i, DateTime d)
        {
            this.Bodovi = b;
            this.Ime = i;
            this.Datum = d;
        }

        public override string ToString()
        {
            //u listboxu vidimo short date, ali u objekt spremamo puni DateTime.Now
            return String.Format("{0} - {1} - {2}", this.Bodovi, this.Ime, this.Datum.ToShortDateString());
        }

        //IComparable na 2 načina:

        //ovako smo radili na vježbama
        //int IComparable.CompareTo(object o)
        //{
        //    Igrac t = (Igrac)o;
        //    if (this.Bodovi < t.Bodovi)
        //        return 1;
        //    else if (this.Bodovi > t.Bodovi)
        //        return -1;
        //    else
        //        return 0;
        //}

        //pošto je za liste koje sadrže varijable tipa int već podržana metoda CompareTo(),
        //napravimo cast objekta o u instancu klase Igrac i usporedimo samo vrijednosti svojstva Bodovi tipa int.
        //ako želimo obrnuto sortiranje, zamijenimo mjesta objekata koje uspoređujemo.
        int IComparable.CompareTo(object o)
        {
            Igrac t = (Igrac)o;
            return t.Bodovi.CompareTo(this.Bodovi);
        }
    }
}
