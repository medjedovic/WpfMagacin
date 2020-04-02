using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace WpfMagacin
{
    /// <summary>
    /// Interaction logic for WinAESirovina.xaml
    /// </summary>
    public partial class WinAESirovina : Window
    {
        public List<clsSirovina> sirovine;

        public string sifra { get; set; }
        public string naziv { get; set; }

        public WinAESirovina(List<clsSirovina> s)
        {
            InitializeComponent();
            //instanca klase osoba u datacontext zbog bindinga
            DataContext = new clsSirovina();

            //sve bindinge odjednom commituje
            BindingGroup = new BindingGroup();
            sirovine = s;

            txtSifra.DataContext = this;
            txtNaziv.DataContext = this;

        }

        private void Unesi(object sender, RoutedEventArgs e)
        {
            if (BindingGroup.CommitEdit())
            {
                foreach (clsSirovina os in sirovine)
                {
                    if (os.sifra == sifra)
                    {
                        MessageBox.Show("Sirovina sa tom sifrom VEĆ postoji!");
                        return;
                    }
                    if (os.naziv == naziv)
                    {
                        MessageBox.Show("Sirovina sa tim nazivom VEĆ postoji!");
                        return;
                    }
                }
                //MessageBox.Show("Sirovina sa tim imenom ili sifrom NE postoji!");
                //na ovaj način kažemo da je unos urađen i da je sve ok
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Došlo je do greške prilikom unosa podataka u edit poljima!");
            }


            //if (BindingGroup.CommitEdit())
            //{

            //    DialogResult = true;
            //    Close();
            //}
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
