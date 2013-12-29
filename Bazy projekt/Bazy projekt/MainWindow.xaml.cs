
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bazy_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login(object sender, MouseButtonEventArgs e)
        {
            Directory.CreateDirectory(@"Music");
            Directory.CreateDirectory(@"Awatars");
            using (StreamWriter sw = new StreamWriter(@"Awatars/Test.txt")) 
        {           
            sw.Write("Writing Text ");
            sw.WriteLine("Here we go.");
            sw.WriteLine("-------------------");
            sw.Write("Today is is: " + DateTime.Now);
            sw.WriteLine("Done");
        }


            //if (checkLoginAndPassword(loginTextBox.Text, passwordBox.Password))
            //{
            //    DashboardWindow w = new DashboardWindow();
            //    w.Show();
            //    this.Close();
            //}

        }

        private bool checkLoginAndPassword(String login, String haslo)
        {
            UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
            Model.UżytkownicyDataTable users = adapter.GetData();

            if (!users.Any(row => (row.Login == login && row.Hasło == haslo)))
            {
                alertMainWidnow.Text = "Dane logowania nieprawidłowe";
                return false;
            }
            return true;
        }



        //void metodaTutorialowa()
        //{
        //    ///Tu mamy tabele uzytkownikow
        //    Model.UżytkownicyDataTable users = new Model.UżytkownicyDataTable();
        //    //Mamy metody takie jak szukanie, deletowanie itp.
        //    //users.FindByLogin;
        //    //chcemy dodac usera. tworzymy nowy rekord
        //    Model.UżytkownicyRow uzytkownik = users.NewUżytkownicyRow();
        //    //uzupelniamy dane
        //    uzytkownik.Login = "test";
        //    uzytkownik.DataRejestracji = DateTime.Now;
        //    uzytkownik.Email = "";
        //    //dodajemy do tabeli
        //    users.AddUżytkownicyRow(uzytkownik);
        //    //zapis do bazy zmian(taki commit)
        //    users.AcceptChanges();

        //    //inne przykłady:
        //    //kasowanie usera
        //    users.FindByLogin("testowy").Delete();
        //    //updatowanie usera
        //    users.FindByLogin("testowy").Saldo += 200;//zwiekszamy saldo :D

        //    //PAMIETAMY O ZAPISIE DO BAZY PO KAZDEJ operacji!!!!!!!
        //    users.AcceptChanges();//!!!!!!!!!!!!!!!!!
        //}
    }
}
