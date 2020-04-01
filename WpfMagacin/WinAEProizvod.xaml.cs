using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMagacin
{
    /// <summary>
    /// Interaction logic for WinAESirovina.xaml
    /// </summary>
    public partial class WinAEProizvod : Window
    {
        public WinAEProizvod()
        {
            InitializeComponent();

            //instanca klase osoba u datacontext zbog bindinga
            DataContext = new clsProizvod();

            //sve bindinge odjednom commituje
            BindingGroup = new BindingGroup();
        }

        private void Unesi(object sender, RoutedEventArgs e)
        {
            if (BindingGroup.CommitEdit())
            {
                DialogResult = true;
                Close();
            }
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
