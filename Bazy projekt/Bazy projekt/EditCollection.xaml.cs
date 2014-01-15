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
    /// Interaction logic for EditCollection.xaml
    /// </summary>
    public partial class EditCollection : Window
    {
        public EditCollection()
        {
            InitializeComponent();
            Model.KolekcjeRow kolekcja = SessionSingleton.aktualnaKolekcja;
            dodajUtworTextBoxCena.Text = kolekcja.CenaKolekcji;
            dodajUtworTextBoxNazwaUtworu.Text = kolekcja.Nazwa;
            dodajUtworTextBoxOpisUtworu.Text = kolekcja.Opis;
        }

        private void zapiszEdycjeKolekcji(object sender, MouseButtonEventArgs e)
        {
            KolekcjeTableAdapter adapter = new KolekcjeTableAdapter();
            Model.KolekcjeDataTable kolekcje = adapter.GetData();
            Bazy_projekt.Model.KolekcjeRow kolekcja = kolekcje.FindByIDKolekcji(SessionSingleton.aktualnaKolekcja.IDKolekcji);

            kolekcja.Nazwa = dodajUtworTextBoxNazwaUtworu.Text;
            kolekcja.CenaKolekcji = dodajUtworTextBoxCena.Text;
            kolekcja.Opis = dodajUtworTextBoxOpisUtworu.Text;

            //utwory.AcceptChanges(piosenka);
            adapter.Update(kolekcje);
            MessageBox.Show("Zapisano!");
            DashboardWindow win = new DashboardWindow();
            win.Show();
            this.Close();
        }
    }
}
