using Bazy_projekt.ModelTableAdapters;
using Bazy_projekt.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            registerWindowKomunikatOBledzie.Text = "";

        }

        private void pokazRejestracjeKlienta(object sender, MouseButtonEventArgs e)
        {
            wykonawcaRejestracja.Visibility = System.Windows.Visibility.Collapsed;
            klientRejestracja.Visibility = System.Windows.Visibility.Visible;
            wykonawcaZakladka.Opacity = 0.5;
            klientZakladka.Opacity = 0.75;
            registerWindowKomunikatOBledzie.Text = "";
        }

        private void pokazRejestracjeWykonawcy(object sender, MouseButtonEventArgs e)
        {
            wykonawcaRejestracja.Visibility = System.Windows.Visibility.Visible;
            klientRejestracja.Visibility = System.Windows.Visibility.Collapsed;
            wykonawcaZakladka.Opacity = 0.75;
            klientZakladka.Opacity = 0.5;
            registerWindowKomunikatOBledzie.Text = "";
        }

        private void rejestrujKlienta(object sender, MouseButtonEventArgs e)
        {
            registerWindowKomunikatOBledzie.Text = "";
            try
            {
                UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
                Model.UżytkownicyDataTable users = adapter.GetData();
                Bazy_projekt.Model.UżytkownicyRow uzytkownik = users.NewUżytkownicyRow();
                uzytkownik.Login = loginTextBoxKlient.Text;
                uzytkownik.Hasło = hasloTextBoxKlient.Password;
                uzytkownik.Email = emailTextBoxKlient.Text;
                uzytkownik.Imię = imieTextBoxKlient.Text;
                uzytkownik.Nazwisko = nazwiskoTextBoxKlient.Text;
                uzytkownik.DataUrodzenia = (DateTime)dataUrodzeniaKlient.SelectedDate;
                uzytkownik.Awatar = "none";
                uzytkownik.Saldo = 0;
                uzytkownik.DataRejestracji = DateTime.Now;
                uzytkownik.IDUprawnienia = 3;
                ValidateKlient(users, uzytkownik);
                users.AddUżytkownicyRow(uzytkownik);
                adapter.Update(users);
                MessageBox.Show("Dodano użytkownika! Możesz się zalogować.");
                MainWindow w = new MainWindow();
                w.Show();
                this.Close();
            }
            catch (ValidationException ex)
            {
                registerWindowKomunikatOBledzie.Text = ex.Message;
            }
        }

        private void rejestracjaWykonawca(object sender, MouseButtonEventArgs e)
        {
            registerWindowKomunikatOBledzie.Text = "";
            try
            {
                UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
                Model.UżytkownicyDataTable users = adapter.GetData();
                Bazy_projekt.Model.UżytkownicyRow uzytkownik = users.NewUżytkownicyRow();
                uzytkownik.Login = loginTextBoxWykonawca.Text;
                uzytkownik.Hasło = hasloTextBoxWykonawca.Password;
                uzytkownik.Email = emailTextBoxWykonawca.Text;
                uzytkownik.Imię = imieTextBoxWykonawca.Text;
                uzytkownik.Nazwisko = nazwiskoTextBoxWykonawca.Text;
                uzytkownik.DataUrodzenia = (DateTime)dataUrodzeniaWykonawca.SelectedDate;
                uzytkownik.Awatar = "none";
                uzytkownik.Saldo = 0;
                uzytkownik.DotychczasowyZysk = 0;

                long temp;
                if (!long.TryParse(numerKontaTextBoxWykonawca.Text.Replace(" ", ""), out temp))
                    throw new ValidationException("Podano zły numer konta!!");
                uzytkownik.NumerKonta = numerKontaTextBoxWykonawca.Text.Replace(" ", "");
                uzytkownik.Miejscowość = miejscowoscTextBoxWykonawca.Text;
                uzytkownik.Ulica = ulicaTextBoxWykonawca.Text;
                uzytkownik.KodPocztowy = kodPocztowy1TextBoxWykonawca.Text + "-" + kodPocztowy2TextBoxWykonawca.Text;
                uzytkownik.DataRejestracji = DateTime.Now;
                uzytkownik.IDUprawnienia = 4;
                ValidateKlient(users, uzytkownik);
                ValidateWykonawca(users, uzytkownik);
                //uzytkownik.Hasło = HasherManager.SetHash(uzytkownik.Hasło);
                users.AddUżytkownicyRow(uzytkownik);
                adapter.Update(users);
                MessageBox.Show("Dodano użytkownika! Możesz się zalogować.");
                MainWindow w = new MainWindow();
                w.Show();
                this.Close();
            }
            catch (ValidationException ex)
            {
                registerWindowKomunikatOBledzie.Text = ex.Message;
            }
            catch (Exception ex)
            {
                registerWindowKomunikatOBledzie.Text = "Sprawdz dane";
            }

        }

        /// <summary>
        /// Tu mamy walidowanie tego co jest TYLKO dla wykonawcy. wspolne bedzie na dole
        /// Dotychczasowy zysk
        /// Numer konta
        /// 
        /// </summary>
        /// <param name="user"></param>
        private void ValidateWykonawca(Model.UżytkownicyDataTable users, Bazy_projekt.Model.UżytkownicyRow user)
        {
            string message = "";

            if (user.Ulica.Equals(""))
                message += "Podaj ulicę" + " , ";
            if (user.Miejscowość.Equals(""))
                message += "Podaj miejscowość" + " , ";
            if (user.KodPocztowy.Equals("-"))
                message += "Podaj kod pocztowy" + " , ";
            if (user.Nazwisko.Length < 3)
                message += "Nazwisko jest za krótkie!" + " , ";

            if (!message.Equals("")) throw new ValidationException(message);
        }

        /// <summary>
        /// Tutaj mamy walidowanie tego co jest wspolnego
        /// Login
        /// Haslo
        /// Email
        /// Imie
        /// Nazwisko
        /// </summary>
        /// <param name="user"></param>
        private void ValidateKlient(Model.UżytkownicyDataTable users, Bazy_projekt.Model.UżytkownicyRow user)
        {
            string message = "";

            if (users.Any(row => row.Login == user.Login))
                message += "Login jest zajęty!" + " , ";
            if (user.Login.Length < 5)
                message += "Login jest za krótki!" + " , ";
            if (user.Imię.Length < 3)
                message += "Imię jest za krótkie!" + " , ";
            if (user.Hasło.Length < 5)
                message += "Hasło jest za krótkie!" + " , ";
            if (user.Nazwisko.Length < 3)
                message += "Nazwisko jest za krótkie!" + " , ";
            if (!user.Email.Contains("@") && !Regex.IsMatch(user.Email,"^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$;"))
                message += "Email jest nieprawidłowy!" + " , ";

            if (!message.Equals("")) throw new ValidationException(message);
        }

        private void wsteka(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
