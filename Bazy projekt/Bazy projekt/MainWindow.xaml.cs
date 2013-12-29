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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bazy_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();			
        }

        private void ustawGwiazdki(object sender, TextChangedEventArgs e)
        {
            //TODO fokus na koncu ustawic
            TextBox textBox = (TextBox)sender;
            int liczbaZnakow = textBox.Text.Length;
            String tmp = "";
            for (int i = 0; i < liczbaZnakow; i++)
            {
                tmp += "*";
            }
            textBox.Text = tmp;
        }

        private void login(object sender, MouseButtonEventArgs e)
        {
            String allertMessage = this.checkLoginAndPassword(loginTextBox.Text, passwordBox.Password);
            if (allertMessage.Equals(""))
            {
                alertMainWidnow.Text = allertMessage;
                //TODO nawiguj dalej
                _mainFrame.NavigationService.Navigate(new DashboardPage());
            }else{
                // cos poszlo nie tak
                alertMainWidnow.Text = allertMessage;
            }

              /*
               * Nawigacja dla Windows dzialajaca
               * 
              DashboardWindow w = new DashboardWindow();
              w.Show();
               * */
        }

        private string checkLoginAndPassword(String login, String haslo)
        {
            if (login.Length <= 4 || login.Length>30)
            {
                return "Login musi składać się przynajniej z 5 znaków a maksymalnie z 30";
            }

            if (haslo.Length <= 4 || haslo.Length > 30)
            {
                return "Haslo musi składać się przynajniej z 5 znaków a maksymalnie z 30";
            }
            //TODO sprawdzenie czy taki uzytkownik isteniej w bazie
            //TODO sprawdzenie czy dla danego uzytkownika wpisano poprawne haslo
            return "";
        }
    }
}
