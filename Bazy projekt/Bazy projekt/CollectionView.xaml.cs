using Bazy_projekt.ModelTableAdapters;
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
    /// Interaction logic for CollectionView.xaml
    /// </summary>
    public partial class CollectionView : Window
    {
        MediaPlayer player;

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


        }

        private void utworyKolekcji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SessionSingleton.aktualnyUtwor = utworyKolekcji.SelectedItem as Model.UtworyRow;
        }


    }
}
