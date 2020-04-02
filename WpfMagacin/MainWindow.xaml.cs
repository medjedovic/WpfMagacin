using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Linq;


namespace WpfMagacin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<clsSirovina> lstSirovina = new ObservableCollection<clsSirovina>();
        public ObservableCollection<clsProizvod> lstProizvod = new ObservableCollection<clsProizvod>();

        public MainWindow()
        {
            InitializeComponent();
            //proglašavanje datacontexta sam prozor i naći će pretragu property u datacontextu
            //DataContext = this;

            ////Ukoliko file postoji otvori ako ne samo prikaži listu
            //if (File.Exists("ListaProizvoda.dat"))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    using (FileStream fs = new FileStream("ListaProizvoda.dat", FileMode.Open, FileAccess.Read))
            //    {
            //        lstProizvod = bf.Deserialize(fs) as ObservableCollection<clsProizvod>;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Falj ne postoji!");
            //}

            lstProizvod.Add(new clsProizvod("001", "jaffa torta", 800, 300, 20, 3));
            lstProizvod.Add(new clsProizvod("002", "grilijaš", 1200, 500, 30, 5));
            lstProizvod.Add(new clsProizvod("003", "baklava", 2000, 1000, 35, 5));
            lstProizvod.Add(new clsProizvod("004", "trileće", 800, 200, 20, 2));
            dgProizvodi.ItemsSource = lstProizvod;

            lstSirovina.Add(new clsSirovina("001", "mlevena plazma", 190, 10));
            lstSirovina.Add(new clsSirovina("002", "šećer sitni", 85, 10));
            lstSirovina.Add(new clsSirovina("003", "slatka pavlaka", 120, 5));
            lstSirovina.Add(new clsSirovina("004", "čokolada za kuvanje", 100, 25));
            lstSirovina.Add(new clsSirovina("005", "orah mrezga", 800, 1));
            lstSirovina.Add(new clsSirovina("006", "lešnik mrezga", 2300, 1));
            lstSirovina.Add(new clsSirovina("007", "karamela", 200, 1));
            dgSirovine.ItemsSource = lstSirovina;

        }


        private string pretraga;
        public string Pretraga
        {
            get
            {
                return pretraga;
            }

            set
            {
                pretraga = value;
                //IsNullOrEmpty() da li je string u potupunosti prazan ""
                //null je nema vrijednosti i ne postoji
                //IsNullOrWhiteSpace() da li je string u ili ima samo praznine "    "

                //ukoliko nije nullorempty i nije nullorwhitespace
                if (!(string.IsNullOrEmpty(pretraga) || string.IsNullOrWhiteSpace(pretraga)))
                {
                    ObservableCollection<clsProizvod> rezltatPretrage = new ObservableCollection<clsProizvod>();

                    foreach (clsProizvod o in lstProizvod)
                    {
                        if ((o.sifra != null && o.sifra.ToLower().Contains(pretraga.ToLower())) || (o.naziv != null && o.naziv.ToLower().Contains(pretraga.ToLower())))
                        {
                            rezltatPretrage.Add(o);
                        }
                    }
                    dgProizvodi.ItemsSource = rezltatPretrage;
                }
                else
                {
                    dgProizvodi.ItemsSource = lstProizvod;
                }
            }
        }
        

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgSirovine_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgSirovine.SelectedItems.Count == 1)
            {
                btnIzmena.IsEnabled = true;
                btnP.IsEnabled = true;
                btnM.IsEnabled = true;

            }
            else
            {
                btnIzmena.IsEnabled = false;
                btnP.IsEnabled = false;
                btnM.IsEnabled = false;
            }
        }

        

        private void DodavanjeSirovine(object sender, RoutedEventArgs e)
        {
            WinAESirovina waes = new WinAESirovina(lstSirovina.ToList());
            waes.Owner = this;
            if (waes.ShowDialog() == true)
            {
                lstSirovina.Add(waes.DataContext as clsSirovina);
            }
        }

        private void IzmenaSirovine(object sender, RoutedEventArgs e)
        {
            if (dgSirovine.SelectedItem != null)
            {
                WinAESirovina waes = new WinAESirovina(lstSirovina.ToList());
                waes.Owner = this;
                waes.DataContext = dgSirovine.SelectedItem;
                waes.ShowDialog();
            }
        }

        private void BrisanjeSirovine(object sender, RoutedEventArgs e)
        {
            if (dgSirovine.SelectedItem != null)
            {
                List<clsSirovina> zaBrisanje = new List<clsSirovina>();
                //foreach je protektivan i ne dozvoljava brisanje sa ObservableCollection na kojoj radi
                foreach (clsSirovina selektovano in dgSirovine.SelectedItems)
                {
                    //dodaje na listu sve selektovanje jer ne može da ih briše
                    zaBrisanje.Add(selektovano);
                }

                foreach (clsSirovina o in zaBrisanje)
                {
                    //briše sve sa listeO listu koja je obeležena zaBrisanje
                    lstSirovina.Remove(o);
                    //brisanje liste .clear()
                }
            }

            if (dgSirovine.SelectedItem != null)
            {
                lstSirovina.Remove(dgSirovine.SelectedItem as clsSirovina);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //kodiranje podataka u fajlu
            BinaryFormatter bf = new BinaryFormatter();
            //using je svestan da koristimo filestream u ovom bloku i sam kada zavtši sa radom on će zatvoriti 
            //fajlove sa kojima je radio
            using (FileStream kaFajlu = new FileStream("ListaProizvoda.dat", FileMode.Create, FileAccess.Write))
            {
                bf.Serialize(kaFajlu, lstProizvod);
            }
            //ovo encrypt će zaključati fajl sa lokalnim userom računara i sa drugim korisnikom neće moći da se otvori
            File.Encrypt("ListaProizvoda.dat");
        }

        private void PlusKSirovine(object sender, RoutedEventArgs e)
        {
            if (dgSirovine.SelectedItem != null)
            {
                (dgSirovine.SelectedItem as clsSirovina).kolicina++;
                (dgSirovine.SelectedItem as clsSirovina).naziv = (dgSirovine.SelectedItem as clsSirovina).kolicina.ToString();
            }
        }

        private void MinusKSirovine(object sender, RoutedEventArgs e)
        {
            if (dgSirovine.SelectedItem != null)
            {
                (dgSirovine.SelectedItem as clsSirovina).kolicina--;
                (dgSirovine.SelectedItem as clsSirovina).naziv = (dgSirovine.SelectedItem as clsSirovina).kolicina.ToString();
            }
        }

        private void DodavanjeProizvoda(object sender, RoutedEventArgs e)
        {
            WinAEProizvod waep = new WinAEProizvod();
            waep.Owner = this;
            if (waep.ShowDialog() == true)
            {
                lstProizvod.Add(waep.DataContext as clsProizvod);
            }
        }

        private void IzmenaProizvoda(object sender, RoutedEventArgs e)
        {
            if (dgProizvodi.SelectedItem != null)
            {
                WinAEProizvod waep = new WinAEProizvod();
                waep.Owner = this;
                waep.DataContext = dgProizvodi.SelectedItem;
                waep.ShowDialog();
            }
        }

        private void BrisanjeProizvoda(object sender, RoutedEventArgs e)
        {
            if (dgProizvodi.SelectedItem != null)
            {
                lstProizvod.Remove(dgProizvodi.SelectedItem as clsProizvod);
            }
        }
    }
}
