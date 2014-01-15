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
        public CollectionView()
        {
            InitializeComponent();


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
    }
}
