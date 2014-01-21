using Bazy_projekt.ModelTableAdapters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    /// Interaction logic for SongWindow.xaml
    /// </summary>
    public partial class SongWindow : Window
    {
        MediaPlayer player;
        bool setPlayIT = false;
        public SongWindow()
        {
            InitializeComponent();

            Model.UtworyRow aktualnyUtwor = SessionSingleton.aktualnyUtwor;
            //TODO sprawdzic czy uzytkownik ma kupiona dana piosenke jezeli tak to od razu wyswietlic - > pobierzButton.Visibility = System.Windows.Visibility.Visible;

            //TODO uzupelnic to ponizej
            songViewOpis.Text = aktualnyUtwor.Opis;
            songViewAutor.Text = "Autor: " + aktualnyUtwor.Login;
            songViewCena.Text = "Cena: " + aktualnyUtwor.Cena;
            songViewNazwaUtworu.Text = aktualnyUtwor.Nazwa;

            int x = Int16.Parse(aktualnyUtwor.Ocena);
            ustawOcene(x);

            Uri uri = new Uri(@"Music/" +
    SessionSingleton.aktualnyUtwor.IDUtworu + SessionSingleton.aktualnyUtwor.Format, UriKind.Relative);
            player = new MediaPlayer();
            player.Open(uri);

            pobierzButton.Visibility = Visibility.Hidden;
            pauseSong(null, null);
        }

        private void pobierzPiosenke(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = SessionSingleton.aktualnyUtwor.Nazwa; // Default file name
            dlg.DefaultExt = SessionSingleton.aktualnyUtwor.Format; // Default file extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                File.Copy(@"Music/" + SessionSingleton.aktualnyUtwor.IDUtworu + SessionSingleton.aktualnyUtwor.Format, filename);
            }

        }

        private void kupPiosenke(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ZamówieniaTableAdapter adapterZamowienia = new ZamówieniaTableAdapter();


                Model.ZamówieniaDataTable zamowienia = adapterZamowienia.GetData();
                Bazy_projekt.Model.ZamówieniaRow zamowieniaRow = zamowienia.NewZamówieniaRow();

                zamowieniaRow.CzyUtwór = true;
                zamowieniaRow.DataZakupu = DateTime.Now;
                zamowieniaRow.Login = SessionSingleton.zalogowanyUser.Login;
                zamowieniaRow.Status = true;
                zamowieniaRow.IDUtworu = SessionSingleton.aktualnyUtwor.IDUtworu;

                zamowienia.AddZamówieniaRow(zamowieniaRow);
                adapterZamowienia.Update(zamowienia);

                MessageBox.Show("Piosenka Kupiona, możesz pobrać piosenkę na dysk");
                pobierzButton.Visibility = Visibility.Visible;

                //Dodanie kasy dla autora                   
                UżytkownicyTableAdapter adapterUzytkownicy = new UżytkownicyTableAdapter();
                Model.UżytkownicyDataTable uzytkownicy = adapterUzytkownicy.GetData();
                Bazy_projekt.Model.UżytkownicyRow uzytkownik =
                    uzytkownicy.FindByLogin(SessionSingleton.aktualnyUtwor.Login);
                uzytkownik.Saldo += SessionSingleton.aktualnyUtwor.Cena;

                adapterUzytkownicy.Update(uzytkownicy);
            }
            catch (Exception ex) { }

        }

        private bool ustawOcene(int x)
        {
            star1.Visibility = System.Windows.Visibility.Hidden;
            star2.Visibility = System.Windows.Visibility.Hidden;
            star3.Visibility = System.Windows.Visibility.Hidden;
            star4.Visibility = System.Windows.Visibility.Hidden;
            star5.Visibility = System.Windows.Visibility.Hidden;

            if (x >= 1)
                star1.Visibility = System.Windows.Visibility.Visible;
            if (x >= 2)
                star2.Visibility = System.Windows.Visibility.Visible;
            if (x >= 3)
                star3.Visibility = System.Windows.Visibility.Visible;
            if (x >= 4)
                star4.Visibility = System.Windows.Visibility.Visible;
            if (x >= 5)
                star5.Visibility = System.Windows.Visibility.Visible;

            return true;

        }

        private void playSong(object sender, MouseButtonEventArgs e)
        {
            if (!setPlayIT)
                return;
            pauseSongButton.Visibility = System.Windows.Visibility.Visible;
            playSongButton.Visibility = System.Windows.Visibility.Collapsed;

            player.Play();

        }

        private void pauseSong(object sender, MouseButtonEventArgs e)
        {
            pauseSongButton.Visibility = System.Windows.Visibility.Collapsed;
            playSongButton.Visibility = System.Windows.Visibility.Visible;
            player.Pause();
        }

        private void back(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            DashboardWindow dw = new DashboardWindow();
            dw.Show();
            this.Close();
        }

        private void edytujPiosenke(object sender, MouseButtonEventArgs e)
        {
            EditSongg son = new EditSongg();
            son.Show();
            this.Close();
        }

        private void setPlay(object sender, MouseButtonEventArgs e)
        {
            setPlayIT = true;
        }
    }
}
