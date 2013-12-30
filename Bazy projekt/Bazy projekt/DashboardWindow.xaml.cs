using Bazy_projekt.ModelTableAdapters;
using Bazy_projekt.Other;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Visible;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;

          // loginTextBox2.Text = SessionSingleton.zalogowanyUser.Login;
         //   saldoTextBox.Text = SessionSingleton.zalogowanyUser.Saldo.ToString();

            gridData.Items.Clear();

            UtworyTableAdapter adapter = new UtworyTableAdapter();
            Model.UtworyDataTable utwory = adapter.GetData();
            Bazy_projekt.Model.UtworyRow piosenka = utwory.NewUtworyRow();

            piosenka.Login = "Krol Cipek";
            piosenka.Nazwa = "Siala baba mak, nie wiedziala jak";
            piosenka.RokPowstania = 1992;
            piosenka.Cena = 562;
            piosenka.Ocena = "0";
            piosenka.Opis = "opis";
            piosenka.Format = ".mp3";
            piosenka.KategoriaWiekowa = "+16";

            gridData.Items.Add(piosenka);

            

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }


        #region Utwory i kolekcje

        #endregion Utwory i kolekcje

        #region MOJE Utwory i kolekcje

        #endregion MOJE Utwory i kolekcje

        #region Dodaj Utwór
        string pathDoUtworu = "";
        private void wybierzPiosenkeZDysku(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            dlg.DefaultExt = ".mp3";


            // Get the selected file name and display in a TextBox 
            if (dlg.ShowDialog() == true)
            {
                // Open document 
                dodajUtworTextBoxWybierzPlik.Text = dlg.FileName;
                pathDoUtworu = dlg.FileName;
            }
        }

        private void dodajUtworDoBazy(object sender, MouseButtonEventArgs e)
        {
            //TODO tutaj dodawanie utworu do korzystac z tego:
            // dodajUtworRokPowstania, dodajUtworTextBoxCena, dodajUtworTextBoxNazwaUtworu, dodajUtworTextBoxOpisUtworu, dodajUtworTextBoxWybierzPlik

            try
            {
                UtworyTableAdapter adapter = new UtworyTableAdapter();
                Model.UtworyDataTable utwory = adapter.GetData();
                Bazy_projekt.Model.UtworyRow piosenka = utwory.NewUtworyRow();

                piosenka.Login = SessionSingleton.zalogowanyUser.Login;
                piosenka.Nazwa = dodajUtworTextBoxNazwaUtworu.Text;
                piosenka.RokPowstania = short.Parse(dodajUtworRokPowstania.SelectedDate.Value.Year.ToString());
                piosenka.Cena = int.Parse(dodajUtworTextBoxCena.Text);
                piosenka.Ocena = "0";
                piosenka.Opis = dodajUtworTextBoxOpisUtworu.Text;
                piosenka.Format = "."+pathDoUtworu.Split('.')[pathDoUtworu.Split('.').Length - 1];
                piosenka.KategoriaWiekowa = "+16";

                //ValidateKlient(users, uzytkownik);
                utwory.AddUtworyRow(piosenka);
                adapter.Update(utwory);
                //zapis pliku do folderu
                File.Copy(pathDoUtworu, @"Music/" + adapter.GetData().Last().IDUtworu + piosenka.Format);
                
            }
            catch (ValidationException ex)
            {
                dodajUtworKomunikatOBledzie.Text = ex.Message;
            }
        }

        #endregion Dodaj Utwór

        #region Dodaj Kolekcję
        private void dodanieUtworuDoKolekcji(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie zaznaczonego utworu do kolekcji
        }

        private void usunUtworZKolekcji(object sender, MouseButtonEventArgs e)
        {
            //TODO usuniecie zaznaczonego na liscie utworu z kolekcji
        }

        private void DodajKolekcjeDoBazy(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie kolekcji do bazy na podstawie: 
            //  dodajKolekcjeTextBoxCenaKolekcji, dodajKolekcjeTextBoxListaNowychUtworow, dodajKolekcjeTextBoxListaUtworowUzytkownika, dodajKolekcjeTextBoxNazwaKolekcji, dodajKolekcjeTextBoxOpisKolekcji
        }

        #endregion Dodaj Kolekcję

        #region zmiany Visilbility
        private void utworyIKolekcje(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Visible;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Visible;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;

            gridData.Items.Clear();
            //TODO tutaj wypelnic gridData wszystkimi utworami
        }

        private void dodajUtwor(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Hidden;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Visible;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dodajKolekcje(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Hidden;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Visible;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        }

        private void mojeUtworyIKolekcje(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Visible;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Visible;

            gridData.Items.Clear();
            //TODO tutaj wypelnic gridData TYLKO utworami uzytkownika
        }

        #endregion zmiany Visibility
    }
}
