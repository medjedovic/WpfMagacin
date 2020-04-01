using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMagacin
{
    [Serializable]
    public class clsSirovina : INotifyPropertyChanged
    {
        //Atribut i odnosi se na klasu ispod njih
        public string sifra { get; set; }
        public string naziv { get; set; }

        private decimal k;
        public decimal kolicina
        {
            get
            {
                return k;
            }

            set
            {
                k = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaK"));
            }
        }


        private decimal c;
        public decimal cijena
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaK"));
            }
        }

        /// <summary>
        /// Blanko konstruktor instance klase
        /// </summary>

        public clsSirovina() { }
        /// <summary>
        /// konstruktor sa parametrima
        /// </summary>
        /// <param name="s">varijabla za dodjelu šifre sirovine</param>
        /// <param name="n">varijabla za dodjelu naziva sirovine</param>
        /// <param name="c">varijabla za dodjelu cijene sirovine</param>
        /// <param name="k">varijabla za dodjelu kolicine sirovine</param>
        public clsSirovina(string s, string n, decimal c, decimal k)
        {
            sifra = s;
            naziv = n;
            cijena = c;
            kolicina = k;
        }

        /// <summary>
        /// Property koji vraća vrijednost sirovina 
        /// </summary>
        public decimal UCijenaK
        {
            get
            {
                return cijena * kolicina;
            }
        }


        public override string ToString()
        {
            return $"{sifra} {naziv} -> {cijena}€";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
