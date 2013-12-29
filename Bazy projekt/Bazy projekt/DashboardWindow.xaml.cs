﻿using System;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory.Visibility = System.Windows.Visibility.Visible;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void utworyIKolekcje(object sender, MouseButtonEventArgs e)
        {
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory.Visibility = System.Windows.Visibility.Visible;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dodajUtwor(object sender, MouseButtonEventArgs e)
        {
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Visible;
            utwory.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dodajKolekcje(object sender, MouseButtonEventArgs e)
        {
            dodajKolekcje2.Visibility = System.Windows.Visibility.Visible;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Hidden;
        }

        private void mojeUtworyIKolekcje(object sender, MouseButtonEventArgs e)
        {
            dodajKolekcje2.Visibility = System.Windows.Visibility.Hidden;
            dodajUtwor2.Visibility = System.Windows.Visibility.Hidden;
            utwory.Visibility = System.Windows.Visibility.Hidden;
            mojeUtwory.Visibility = System.Windows.Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dodajUtworDoBazy(object sender, MouseButtonEventArgs e)
        {
            //TODO tutaj dodawanie utworu do korzystac z tego:
           // dodajUtworRokPowstania, dodajUtworTextBoxCena, dodajUtworTextBoxNazwaUtworu, dodajUtworTextBoxOpisUtworu, dodajUtworTextBoxWybierzPlik
        }

        private void wybierzPiosenkeZDysku(object sender, MouseButtonEventArgs e)
        {
            //TODO filepicker i piosenka z dysku
        }

        private void usunUtworZKolekcji(object sender, MouseButtonEventArgs e)
        {
            //TODO usuniecie zaznaczonego na liscie utworu z kolekcji
        }

        private void dodanieUtworuDoKolekcji(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie zaznaczonego utworu do kolekcji
        }

        private void DodajKolekcjeDoBazy(object sender, MouseButtonEventArgs e)
        {
            //TODO dodanie kolekcji do bazy na podstawie: 
          //  dodajKolekcjeTextBoxCenaKolekcji, dodajKolekcjeTextBoxListaNowychUtworow, dodajKolekcjeTextBoxListaUtworowUzytkownika, dodajKolekcjeTextBoxNazwaKolekcji, dodajKolekcjeTextBoxOpisKolekcji
        }
    }
}
