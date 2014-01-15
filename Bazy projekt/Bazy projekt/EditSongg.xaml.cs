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
    /// Interaction logic for EditSongg.xaml
    /// </summary>
    public partial class EditSongg : Window
    {
        public EditSongg()
        {
            InitializeComponent();
            Model.UtworyRow aktualnyUtwor = SessionSingleton.aktualnyUtwor;
            dodajUtworTextBoxNazwaUtworu.Text = aktualnyUtwor.Nazwa;
            dodajUtworTextBoxOpisUtworu.Text = aktualnyUtwor.Opis;
            dodajUtworTextBoxCena.Text = aktualnyUtwor.Cena.ToString();

            tb_18.IsChecked = aktualnyUtwor.KategoriaWiekowa.Equals("+18");
            tb_16.IsChecked = aktualnyUtwor.KategoriaWiekowa.Equals("+16");
            tb_12.IsChecked = aktualnyUtwor.KategoriaWiekowa.Equals("+12");
            tb_3.IsChecked = aktualnyUtwor.KategoriaWiekowa.Equals("+3");

        }

        private void zapiszEdycjeUtworu(object sender, MouseButtonEventArgs e)
        {
            UtworyTableAdapter adapter = new UtworyTableAdapter();
            Model.UtworyDataTable utwory = adapter.GetData();
            Bazy_projekt.Model.UtworyRow piosenka = utwory.FindByIDUtworu(SessionSingleton.aktualnyUtwor.IDUtworu);

            piosenka.Login = SessionSingleton.zalogowanyUser.Login;
            piosenka.Nazwa = dodajUtworTextBoxNazwaUtworu.Text;
            piosenka.Cena = int.Parse(dodajUtworTextBoxCena.Text);
            piosenka.Opis = dodajUtworTextBoxOpisUtworu.Text;
            piosenka.KategoriaWiekowa = (bool)tb_18.IsChecked ? "+18" : (bool)tb_16.IsChecked ? "+16" : (bool)tb_12.IsChecked ? "+12" : "+3";

            //utwory.AcceptChanges(piosenka);
            adapter.Update(utwory);
            MessageBox.Show("Zapisano!");
            DashboardWindow win = new DashboardWindow();
            win.Show();
            this.Close();
        }
    }
}
