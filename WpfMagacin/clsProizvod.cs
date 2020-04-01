using System;
using System.ComponentModel;

namespace WpfMagacin
{
	[Serializable]
	public class clsProizvod : INotifyPropertyChanged
    {
		public string sifra { get; set; }
		public string naziv { get; set; }
		//public decimal marza { get; set; }
		//public decimal cijenaSirovina { get; set; }
		//public decimal cijenaRada { get; set; }
		
		private decimal c;
		/// <summary>
		/// Property koji vraća vrijednost izlazne cijene proizvoda
		/// Read Only property
		/// ne cuvamo vrijednost nigde nego je samo računamo svaki put kada se traži
		/// </summary>
		public decimal UCijenaP
		{
			get
			{
				c = (cijenaSirovina + cijenaRada) * (marza / 100 + 1);
				return c;
			}
			set
			{
				c = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		private decimal cr;
		public decimal cijenaRada
		{
			get
			{
				return cr;
			}
			set
			{
				cr = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		private decimal cs;
		public decimal cijenaSirovina
		{
			get
			{
				return cs;
			}
			set
			{
				cs = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		private decimal m;
		[marza: NonSerialized]
		public decimal marza
		{
			get
			{
				return m;
			}
			set
			{
				m = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		private decimal kc;
		[UKCijenaP: NonSerialized]
		public decimal UKCijenaP
		{
			get
			{
				kc = UCijenaP * kolicina;
				return kc;
			}
			set
			{
				kc = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("kolicina"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		private decimal k;
		[kolicina: NonSerialized]
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
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UCijenaP"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UKCijenaP"));
			}
		}

		

		/// <summary>
		/// blanko konstruktor
		/// </summary>
		public clsProizvod() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s">varijabla za dodjelu šifre artikla</param>
		/// <param name="n">varijabla za dodjelu naziv artikla</param>
		/// <param name="uc">varijabla za dodjelu ulazna cijena artikla</param>
		/// <param name="m">varijabla za dodjelu marža artikla</param>
		public clsProizvod(string s, string n, decimal cs, decimal cr, decimal m, decimal k)
		{
			sifra = s;
			naziv = n;
			cijenaSirovina = cs;
			cijenaRada = cr;
			marza = m;
			kolicina = k;
		}

		public override string ToString()
		{
			return $"{sifra} - {naziv} -> {UCijenaP}";
		}

		public event PropertyChangedEventHandler PropertyChanged;
    }
}
