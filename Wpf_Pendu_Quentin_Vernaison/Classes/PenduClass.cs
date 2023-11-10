using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Threading;

namespace Wpf_Pendu_Quentin_Vernaison.Classes

    
{
    
    internal class PenduClass
    {
        MainWindow _mainWindow;

        
        DispatcherTimer timer;
        public PenduClass(MainWindow main)
        {
            
            _mainWindow = main;
        }

       


        // ********************************************************** Debut de nouvelle game **********************************************************
        // ********************************************************** Debut de nouvelle game **********************************************************
        // ********************************************************** Debut de nouvelle game **********************************************************
        // ********************************************************** Debut de nouvelle game **********************************************************
        // ********************************************************** Debut de nouvelle game **********************************************************

        public void StartNewGame()   // Une fonction qui permet de commencer une nouvelle partie et qui choisi un mot aléatoire dans le tableau de mots et qui affiche le mot mystère en ?
        {
            Random random = new Random();
            _mainWindow.motMystere = _mainWindow.mots[random.Next(_mainWindow.mots.Length)].ToUpper();
            _mainWindow.motAffiche = new string('?', _mainWindow.motMystere.Length);
            _mainWindow.TB_Mot.Text = _mainWindow.motAffiche;
            _mainWindow.erreurs = 0;

        }

        public void StartNewGameHardcore() // Une fonction qui permet de commencer une nouvelle partie et qui choisi un mot aléatoire dans le tableau de mots et qui affiche le mot mystère en ?
        {
            Random random = new Random();
            _mainWindow.motMystere = _mainWindow.mots[random.Next(_mainWindow.mots.Length)].ToUpper();
            _mainWindow.motAffiche = new string('_', _mainWindow.motMystere.Length);
            _mainWindow.TB_Mot.Text = _mainWindow.motAffiche;
            _mainWindow.erreurs = 0;            
        }   


        // ********************************************************** Aide **********************************************************
        // ********************************************************** Aide **********************************************************
        // ********************************************************** Aide **********************************************************
        // ********************************************************** Aide **********************************************************
        // ********************************************************** Aide **********************************************************

        public void Aide() // Fonction qui permet de donner une lettre aléatoire du mot mystère et qui ajoute 2 erreurs au compteur d'erreurs et qui désactive le bouton aide

        {
            Random random = new Random();
            int index = random.Next(_mainWindow.motMystere.Length);
            char lettre = _mainWindow.motMystere[index];
            _mainWindow.motAffiche = _mainWindow.motAffiche.Remove(index, 1).Insert(index, lettre.ToString());
            _mainWindow.TB_Mot.Text = _mainWindow.motAffiche;
            _mainWindow.erreurs = _mainWindow.erreurs + 2;
            _mainWindow.PenduImages.Source = new BitmapImage(new Uri("Image/" + _mainWindow.erreurs + ".png", UriKind.Relative));
            _mainWindow.BTN_Aide.IsEnabled = false;
            _mainWindow.LBL_Vie.Content = "Vie: " + (_mainWindow.maxErreurs - _mainWindow.erreurs);
            // Met la lettre utiliser en vert
            foreach (var bouton in _mainWindow.Grille.Children.OfType<Button>())
            {
                if (bouton.Content.ToString() == lettre.ToString())
                {
                    bouton.Foreground = Brushes.Green;
                    bouton.IsEnabled = false;
                }
            }

            
        }

        // ********************************************************** Timer **********************************************************
        // ********************************************************** Timer **********************************************************
        // ********************************************************** Timer **********************************************************
        // ********************************************************** Timer **********************************************************
        // ********************************************************** Timer **********************************************************


        private void Timer_Tick(object sender, EventArgs e) // Un timer qui permet de faire une barre de chargement qui permet de passer à la fenêtre 4 quand elle est remplie
        {
            _mainWindow.Bar.Value += 1;
            if (_mainWindow.Bar.Value == _mainWindow.Bar.Maximum)
            {


                Window4 Window4 = new Window4(_mainWindow);
                Window4.Show();



                _mainWindow.Premiergame = true;
                StopTimer();
            }
        }


        public void StartTimer()
        {
            _mainWindow.Bar.Value = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void StopTimer()
        {
            _mainWindow.Bar.Value = 0;
            timer.Stop();
        }



        // ********************************************************** Restart **********************************************************
        // ********************************************************** Restart **********************************************************
        // ********************************************************** Restart **********************************************************
        // ********************************************************** Restart **********************************************************
        // ********************************************************** Restart **********************************************************

        public void Restart()
        {
            _mainWindow.PenduImages.Source = new BitmapImage(new Uri("Image/0.png", UriKind.Relative));
            StopTimer();
            StartNewGame();
            _mainWindow.LBL_Vie.Content = "Vie: " + (_mainWindow.maxErreurs - _mainWindow.erreurs);
            foreach (var bouton in _mainWindow.Grille.Children.OfType<Button>()) // Réactive les boutons et remet la couleur des boutons avec la couleur initial
            {
                bouton.IsEnabled = true;
                bouton.Foreground = Brushes.Black;

            }
            _mainWindow.Premiergame = true;
        }

        public void Hardcore() //fonction qui fait comme la restart mais qui ne permet pas de voir les ???
        {
            _mainWindow.PenduImages.Source = new BitmapImage(new Uri("Image/0.png", UriKind.Relative));
            StopTimer();
            StartNewGameHardcore();
            _mainWindow.LBL_Vie.Content = "Vie: " + (_mainWindow.maxErreurs - _mainWindow.erreurs);
            foreach (var bouton in _mainWindow.Grille.Children.OfType<Button>()) // Réactive les boutons et remet la couleur des boutons avec la couleur initial
            {
                bouton.IsEnabled = true;
                bouton.Foreground = Brushes.Black;

            }
            _mainWindow.Premiergame = true;

        }   



        // ********************************************************** Blocage **********************************************************
        // ********************************************************** Blocage **********************************************************
        // ********************************************************** Blocage **********************************************************
        // ********************************************************** Blocage **********************************************************
        // ********************************************************** Blocage **********************************************************



        // Fonction qui permet de bloquer les boutons quand le mot est trouvé ou quand le joueur a perdu

        public void BloquerBouton()
        {
            foreach (var bouton in _mainWindow.Grille.Children.OfType<Button>())
            {
                bouton.IsEnabled = false;
            }
        }


        

        
            
        

        

        
             
            
            
            

                                     
        




    }
}
