﻿using System;
using System.Collections.Generic;
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
        }

        private void pobierzPiosenke(object sender, MouseButtonEventArgs e)
        {
            //TODO pobrąc piosenke na dysk

        }

        private void kupPiosenke(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Piosenka Kupiona, możesz pobrać piosenkę na dysk");
            //TODO Dodac piosenke do autora ( zeby mial ja w swoich utworach)
            //TODO Dodac do wykonawcy do jego salda tą kwote

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

            //JAKBY TO RATAJ ZOBACZYL...
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

        private void back(object sender, MouseButtonEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow();
            dw.Show();
            this.Close();
        }
    }
}
