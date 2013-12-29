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

namespace Bazy_projekt
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            wykonawcaRejestracja.Visibility = System.Windows.Visibility.Collapsed;
            wykonawcaZakladka.Opacity = 0.5;
            klientZakladka.Opacity = 0.75;
            
        }

        private void pokazRejestracjeKlienta(object sender, MouseButtonEventArgs e)
        {
            wykonawcaRejestracja.Visibility = System.Windows.Visibility.Collapsed;
            klientRejestracja.Visibility = System.Windows.Visibility.Visible;
            wykonawcaZakladka.Opacity = 0.5;
            klientZakladka.Opacity = 0.75;
        }

        private void pokazRejestracjeWykonawcy(object sender, MouseButtonEventArgs e)
        {
            wykonawcaRejestracja.Visibility = System.Windows.Visibility.Visible;
            klientRejestracja.Visibility = System.Windows.Visibility.Collapsed;
            wykonawcaZakladka.Opacity = 0.75;
            klientZakladka.Opacity = 0.5;
        }

        private void rejestrujKlienta(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie njowego klienta
        }

        private void rejestracjaWykonawca(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie nowego wykonawcy
        }
    }
}
