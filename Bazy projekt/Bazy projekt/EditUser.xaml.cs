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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser()
        {
            InitializeComponent();
            //TODO jezeli klient to ukryj  edytujWykonawce, jezeli wykonawca to ukryj edytujKlienta
            Model.UżytkownicyRow aktualnyUser = SessionSingleton.aktualnyUser;
            if(aktualnyUser.IDUprawnienia == 4){
                //mamy tutaj wykonawce
                edytujKlientaGrid.Visibility = System.Windows.Visibility.Hidden;
                edytujWykonawce.Visibility = System.Windows.Visibility.Visible;


                edytujWykonawceAdresEmail.Text = aktualnyUser.Email;
                edytujWykonawceImie.Text = aktualnyUser.Imię;
                edytujWykonawceNazwisko.Text = aktualnyUser.Nazwisko;
                edytujWykonawceUlica.Text = aktualnyUser.Ulica;
                edytujWykonawceNazwiskoMeijscowosc.Text = aktualnyUser.Miejscowość;
                edytujWykonawceKodpocztowy1.Text = aktualnyUser.KodPocztowy;

            }else{
                edytujWykonawce.Visibility = System.Windows.Visibility.Hidden;
                edytujKlientaGrid.Visibility = System.Windows.Visibility.Visible;

                edytujKlientaAdresEmail.Text = aktualnyUser.Email;
                edytujKlientaimie.Text = aktualnyUser.Imię;
                edytujKlientanazwisko.Text = aktualnyUser.Nazwisko;
            }
  
        }

       

        private void edytujKlienta(object sender, MouseButtonEventArgs e)
        {
            UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
            Model.UżytkownicyDataTable usery = adapter.GetData();
            Bazy_projekt.Model.UżytkownicyRow user = usery.FindByLogin(SessionSingleton.aktualnyUser.Login);

           


            user.Email = edytujKlientaAdresEmail.Text;
            user.Imię = edytujKlientaimie.Text;
            user.Nazwisko = edytujKlientanazwisko.Text;
            

          


            //utwory.AcceptChanges(piosenka);

            //TODO tu jakis tez
            adapter.Update(usery);
            MessageBox.Show("Zapisano!");
            DashboardWindow win = new DashboardWindow();
            win.Show();
            this.Close();
        }

        private void edytujWyk2(object sender, MouseButtonEventArgs e)
        {
            UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
            Model.UżytkownicyDataTable usery = adapter.GetData();
            Bazy_projekt.Model.UżytkownicyRow user = usery.FindByLogin(SessionSingleton.aktualnyUser.Login);        

            user.Email = edytujWykonawceAdresEmail.Text;
            user.Imię = edytujWykonawceImie.Text;
            user.Nazwisko = edytujWykonawceNazwisko.Text;
            user.Ulica = edytujWykonawceUlica.Text;
      
            user.KodPocztowy = edytujWykonawceKodpocztowy1.Text;

         
            //utwory.AcceptChanges(piosenka);

            //TODO tu jakis blad wywala chuj wie
            adapter.Update(usery);
            MessageBox.Show("Zapisano!");
            DashboardWindow win = new DashboardWindow();
            win.Show();
            this.Close();
        }

    }
}
