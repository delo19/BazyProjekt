using Bazy_projekt.ModelTableAdapters;
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
    /// Interaction logic for CollectionView.xaml
    /// </summary>
    public partial class CollectionView : Window
    {
        MediaPlayer player;
        bool setPlayIT = false;
        public CollectionView()
        {
            InitializeComponent();

            try
            {
                songViewNazwaKolekcji.Text = SessionSingleton.aktualnaKolekcja.Nazwa;
                songViewAutor.Text = SessionSingleton.aktualnaKolekcja.Login;

                // tu masz pobieranie listy utworow
                UtworyDlaKolekcjiTableAdapter adapterPrzydzialy = new UtworyDlaKolekcjiTableAdapter();
                Model.UtworyDlaKolekcjiDataTable utwory = adapterPrzydzialy.GetData();
                var przydzialyOk = utwory.Select("IDKolekcji='" + SessionSingleton.aktualnaKolekcja.IDKolekcji + "'");
                pobierzButton.Visibility = Visibility.Hidden;

                for (int i = 0; i < przydzialyOk.Length; i++)
                {
                    utworyKolekcji.Items.Add(przydzialyOk[i]);
                }

                Uri uri = new Uri(@"Music/" +
                SessionSingleton.aktualnyUtwor.IDUtworu + SessionSingleton.aktualnyUtwor.Format, UriKind.Relative);
                player = new MediaPlayer();
                player.Open(uri);

                pauseSong(null, null);
            }
            catch (Exception ex) { }
        }


        private void playSong(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!setPlayIT)
                    return;
                pauseSongButton.Visibility = System.Windows.Visibility.Visible;
                playSongButton.Visibility = System.Windows.Visibility.Collapsed;

                player.Play();
            }
            catch (Exception ex) { }
        }

        private void pauseSong(object sender, MouseButtonEventArgs e)
        {
            try
            {
                pauseSongButton.Visibility = System.Windows.Visibility.Collapsed;
                playSongButton.Visibility = System.Windows.Visibility.Visible;
                player.Pause();
            }
            catch (Exception ex) { }
        }


        private void wsteka(object sender, MouseButtonEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow();
            dw.Show();
            this.Close();
        }

        private void edytujKolekcje(object sender, MouseButtonEventArgs e)
        {
            EditCollection ec = new EditCollection();
            ec.Show();
            this.Close();
        }



        private void kupUtwor(object sender, MouseButtonEventArgs e)
        {
            //ZamówieniaTableAdapter adapterZamowienia = new ZamówieniaTableAdapter();
            //ZamówieniaNaUtworyTableAdapter adapterZamowieniaNaUtwory = new ZamówieniaNaUtworyTableAdapter();

            //Model.ZamówieniaDataTable zamowienia = adapterZamowienia.GetData();
            //Model.ZamówieniaNaUtworyDataTable zamowieniaNaUtwory = adapterZamowieniaNaUtwory.GetData();

            //Bazy_projekt.Model.ZamówieniaRow zamowieniaRow = zamowienia.NewZamówieniaRow();
            //Bazy_projekt.Model.ZamówieniaNaUtworyRow zamowieniaNaUtworyRow= zamowieniaNaUtwory.NewZamówieniaNaUtworyRow();

            //zamowieniaRow.CzyUtwór = true;
            //zamowieniaRow.DataZakupu = DateTime.Now;
            //zamowieniaRow.Login = SessionSingleton.zalogowanyUser.Login;
            //zamowieniaRow.Status = true;

            //adapterZamowienia.Update(zamowienia);

            //zamowieniaNaUtworyRow.IDUtworu = SessionSingleton.aktualnyUtwor.IDUtworu;
            //zamowieniaNaUtworyRow.IDZamówienia = zamowienia.OrderBy(x => x.IDZamówienia).Last().IDZamówienia;

            //adapterZamowieniaNaUtwory.Update(zamowieniaNaUtwory);
            MessageBox.Show("Piosenka Kupiona, możesz pobrać piosenkę na dysk");
            pobierzButton.Visibility = Visibility.Visible;

        }

        private void utworyKolekcji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (player != null)
                    player.Stop();
                Model.UtworyDlaKolekcjiRow aktualny = (utworyKolekcji.SelectedItem as Model.UtworyDlaKolekcjiRow);
                if (aktualny != null)
                {
                    Uri uri = new Uri(@"Music/" +
                        aktualny.Utwory_IDUtworu + aktualny.Format, UriKind.Relative);
                    player = new MediaPlayer();
                    player.Open(uri);
                }
            }
            catch (Exception) { }
        }
        private void setPlay(object sender, MouseButtonEventArgs e)
        {
            setPlayIT = true;
        }

        private void pobierzPiosenke(object sender, MouseButtonEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
             Model.UtworyDlaKolekcjiRow aktualny = (utworyKolekcji.SelectedItem as Model.UtworyDlaKolekcjiRow);
             if (aktualny == null)
                 return;

            dlg.FileName =  aktualny.Nazwa;
            dlg.DefaultExt = aktualny.Format; // Default file extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                File.Copy(@"Music/" + aktualny.Utwory_IDUtworu + aktualny.Format, filename);
            }

        }

    }
}
