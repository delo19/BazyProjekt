using Bazy_projekt.ModelTableAdapters;
using Bazy_projekt.Other;
using System;
using System.Collections.Generic;
using System.Data;
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
        Model.UtworyDataTable utworyDlaKolekcji = new Model.UtworyDataTable();

        public DashboardWindow()
        {
            InitializeComponent();
            //TODO admin
            if (!true)
            {
            }
            else
            {
                dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
                dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
                utwory2.Visibility = System.Windows.Visibility.Visible;
                mojeUtwory.Visibility = System.Windows.Visibility.Hidden;

                loginTextBox2.Text = SessionSingleton.zalogowanyUser.Login;
                //saldoTextBox.Text = SessionSingleton.zalogowanyUser.Saldo.ToString();

                gridData.Items.Clear();

                UtworyTableAdapter adapter = new UtworyTableAdapter();
                Model.UtworyDataTable utwory = adapter.GetData();
                Bazy_projekt.Model.UtworyRow piosenka = utwory.NewUtworyRow();


                gridData.SelectedCellsChanged += gridData_SelectedCellsChanged;
                //  gridData.Items.Add(piosenka);

                Model.UtworyDataTable tab = pobierzUtwory(null, null, null);
                for (int i = 0; i < tab.Count; i++)
                {

                    gridData.Items.Add(tab.ElementAt(i));

                }

                Model.KolekcjeDataTable tabKolekcje = pobierzKolekcje(null, null);
                for (int j = 0; j < tabKolekcje.Count; j++)
                {
                    gridDataKolekcje.Items.Add(tabKolekcje.ElementAt(j));

                }

                gridDataKolekcje.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void gridData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Model.UtworyRow x = (Model.UtworyRow)gridData.Items.GetItemAt(gridData.SelectedIndex);
            SessionSingleton.aktualnyUtwor = x;
            SongWindow w = new SongWindow();
            w.Show();
            this.Close();
        }

        void gridData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }


        #region Utwory i kolekcje

        public Model.UtworyDataTable pobierzUtwory(string nazwa, string wykonawca, int? rokPowstania)
        {
            UtworyTableAdapter adapter = new UtworyTableAdapter();
            Model.UtworyDataTable utwory = adapter.GetData();
            DataTable wynik = utwory.CopyToDataTable();
            bool empty = true;

            string message =
                (!string.IsNullOrEmpty(nazwa) ? "Nazwa='" + nazwa + "'" : "") +
                (!string.IsNullOrEmpty(wykonawca) ? "Login='" + wykonawca + "'" : "") +
                (!string.IsNullOrEmpty(nazwa) ? "RokPowstania='" + rokPowstania + "'" : "");

            //if (!string.IsNullOrEmpty(nazwa) && wynik.Select("Nazwa='" + nazwa + "'").Count()>0)
            //{
            //    wynik = wynik.Select("Nazwa='" + nazwa + "'").CopyToDataTable();
            //    empty = false;
            //}
            //if (!string.IsNullOrEmpty(wykonawca) && wynik.Select("Login='" + wykonawca + "'").Count() > 0)
            //{
            //    wynik = wynik.Select("Login='" + wykonawca + "'").CopyToDataTable();
            //    empty = false;
            //}
            //if (rokPowstania != null && wynik.Select("RokPowstania='" + rokPowstania + "'").Count() > 0)
            //{
            //    wynik = wynik.Select("RokPowstania='" + rokPowstania + "'").CopyToDataTable();
            //    empty = false;
            //}
            Model.UtworyDataTable result = new Model.UtworyDataTable();
            var ob = wynik.Select(message);
            if (ob.Count() > 0)
            {
                wynik = ob.CopyToDataTable();
                result.Merge(wynik);
            }

            return result;

        }

        public Model.KolekcjeDataTable pobierzKolekcje(string nazwa, string wykonawca)
        {
            KolekcjeTableAdapter adapter = new KolekcjeTableAdapter();
            Model.KolekcjeDataTable kolekcje = adapter.GetData();
            DataTable wynik = kolekcje.CopyToDataTable();

            if (!string.IsNullOrEmpty(nazwa) && wynik.Select("Nazwa='" + nazwa + "'").Count() > 0)
                wynik = wynik.Select("Nazwa='" + nazwa + "'").CopyToDataTable();
            if (!string.IsNullOrEmpty(wykonawca) && wynik.Select("Login='" + wykonawca + "'").Count() > 0)
                wynik = wynik.Select("Login='" + wykonawca + "'").CopyToDataTable();


            Model.KolekcjeDataTable result = new Model.KolekcjeDataTable();
            result.Merge(wynik);

            return result;

        }



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
                piosenka.Format = "." + pathDoUtworu.Split('.')[pathDoUtworu.Split('.').Length - 1];
                piosenka.KategoriaWiekowa = "+16";

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

        private void WalidujUtwor(Bazy_projekt.Model.UtworyRow piosenka)
        {

        }

        #endregion Dodaj Utwór

        #region Dodaj Kolekcję
        private void dodanieUtworuDoKolekcji(object sender, MouseButtonEventArgs e)
        {
            var utwor = dodajKolekcjeTextBoxListaUtworowUzytkownika.SelectedItem as Bazy_projekt.Model.UtworyRow;
            if (!utworyDlaKolekcji.Any(x => x.IDUtworu == utwor.IDUtworu))
            {
                utworyDlaKolekcji.Rows.Add(utwor.ItemArray);
                dodajKolekcjeTextBoxListaNowychUtworow.Items.Add(utwor);
            }
        }

        private void usunUtworZKolekcji(object sender, MouseButtonEventArgs e)
        {
            var utwor = dodajKolekcjeTextBoxListaNowychUtworow.SelectedItem as Bazy_projekt.Model.UtworyRow;
            if (utwor != null)
            {
                utworyDlaKolekcji.Single(row => row.IDUtworu == utwor.IDUtworu).Delete();
                dodajKolekcjeTextBoxListaNowychUtworow.Items.Remove(dodajKolekcjeTextBoxListaNowychUtworow.SelectedItem);
            }
        }

        private void DodajKolekcjeDoBazy(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //TODO dodanie kolekcji do bazy na podstawie: 
                //  dodajKolekcjeTextBoxCenaKolekcji, dodajKolekcjeTextBoxListaNowychUtworow, dodajKolekcjeTextBoxListaUtworowUzytkownika, dodajKolekcjeTextBoxNazwaKolekcji, dodajKolekcjeTextBoxOpisKolekcji
                KolekcjeTableAdapter adapter = new KolekcjeTableAdapter();
                Model.KolekcjeDataTable kolekcje = adapter.GetData();
                Bazy_projekt.Model.KolekcjeRow kolekcja = kolekcje.NewKolekcjeRow();
                kolekcja.CenaKolekcji = dodajKolekcjeTextBoxCenaKolekcji.Text;
                kolekcja.Ocena = "0";
                kolekcja.Opis = dodajKolekcjeTextBoxOpisKolekcji.Text;
                kolekcja.Nazwa = dodajKolekcjeTextBoxNazwaKolekcji.Text;
                kolekcja.Login = SessionSingleton.zalogowanyUser.Login;
                kolekcja.LiczbaUtworów = utworyDlaKolekcji.Rows.Count.ToString();

                kolekcje.AddKolekcjeRow(kolekcja);
                adapter.Update(kolekcje);
                kolekcje = adapter.GetData();
                // teraz majac kolekcje mozna zapisac przydziały

                PrzydziałyTableAdapter adapterPrzydzialy = new PrzydziałyTableAdapter();
                Model.PrzydziałyDataTable przydzialy = adapterPrzydzialy.GetData();
                for (int i = 0; i < utworyDlaKolekcji.Count; i++)
                {
                    Model.PrzydziałyRow przydzial = przydzialy.NewPrzydziałyRow();
                    przydzial.IDKolekcji = kolekcje.OrderBy(x => x.IDKolekcji).Last().IDKolekcji;
                    przydzial.IDUtworu = utworyDlaKolekcji[i].IDUtworu;
                    przydzialy.AddPrzydziałyRow(przydzial);
                }
                adapterPrzydzialy.Update(przydzialy);


            }
            catch (ValidationException exc) { }


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

            //TODO tutaj wypelnic gridData wszystkimi utworami  Model.UtworyDataTable tab = pobierzUtwory(null, "delxxxxo19", null);
            Model.UtworyDataTable tab = pobierzUtwory(null, null, null);
            for (int i = 0; i < tab.Count; i++)
            {

                gridData.Items.Add(tab.ElementAt(i));

            }
        }

        private void dodajUtwor(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Hidden;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Visible;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
            gridDataKolekcje.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dodajKolekcje(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Hidden;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Visible;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
            gridDataKolekcje.Visibility = System.Windows.Visibility.Hidden;

            Model.UtworyDataTable tab = pobierzUtwory(null, SessionSingleton.zalogowanyUser.Login, null);
            for (int i = 0; i < tab.Count; i++)
            {
                dodajKolekcjeTextBoxListaUtworowUzytkownika.Items.Add(tab.ElementAt(i));
            }
        }

        private void mojeUtworyIKolekcje(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Visible;
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory2.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Visible;

            gridData.Items.Clear();


            Model.UtworyDataTable tab = pobierzUtwory(null, SessionSingleton.zalogowanyUser.Login, null);
            for (int i = 0; i < tab.Count; i++)
            {

                gridData.Items.Add(tab.ElementAt(i));

            }
        }

        #endregion zmiany Visibility

        private void logout(object sender, MouseButtonEventArgs e)
        {


        }

        private void logout(object sender, RoutedEventArgs e)
        {
            SessionSingleton.zalogowanyUser = null;
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void kolekcjeMojePokaz(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Hidden;
            gridDataKolekcje.Visibility = System.Windows.Visibility.Visible;
            utworyMoje.Opacity = 0.7;
            kolekcjeMoje.Opacity = 1.0;
            mojekolekcjeText.Opacity = 1.0;
            mojeutworyText.Opacity = 0.7;
        }

        private void utworyZakladkaPokaz(object sender, MouseButtonEventArgs e)
        {
            gridData.Visibility = System.Windows.Visibility.Visible;
            gridDataKolekcje.Visibility = System.Windows.Visibility.Hidden;
            kolekcjeMoje.Opacity = 0.7;
            utworyMoje.Opacity = 1;
            mojekolekcjeText.Opacity = 0.7;
            mojeutworyText.Opacity = 1.0;
        }

        private void wybierzUtwor(object sender, SelectionChangedEventArgs e)
        {
            SessionSingleton.aktualnaKolekcja = (Model.KolekcjeRow)gridDataKolekcje.Items.GetItemAt(gridDataKolekcje.SelectedIndex);
            CollectionView collection = new CollectionView();
            collection.Show();
            this.Close();

        }

    }
}
