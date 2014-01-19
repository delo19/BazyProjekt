
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

            Directory.CreateDirectory(@"Music");
            Directory.CreateDirectory(@"Awatars");
        }

        private void login(object sender, MouseButtonEventArgs e)
        {

            if (checkLoginAndPassword(loginTextBox.Text, passwordBox.Password))
            {
                DashboardWindow w = new DashboardWindow();
                w.Show();
                this.Close();
            }

        }

        private bool checkLoginAndPassword(String login, String haslo)
        {
            UżytkownicyTableAdapter adapter = new UżytkownicyTableAdapter();
            Model.UżytkownicyDataTable users = adapter.GetData();

            if (!users.Any(row => (row.Login == login && row.Hasło == haslo))) // HasherManager.SetHash(haslo))))
            {
                alertMainWidnow.Text = "Dane logowania nieprawidłowe";
                return false;
            }
            SessionSingleton.zalogowanyUser = users.FindByLogin(login);
            return true;
        }

        private void register(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow w = new RegisterWindow();
            w.Show();
            this.Close();
        }



    }
}
