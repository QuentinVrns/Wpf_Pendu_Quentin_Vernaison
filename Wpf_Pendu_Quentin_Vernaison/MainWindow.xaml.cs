using System;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Wpf_Pendu_Quentin_Vernaison.Classes;

namespace Wpf_Pendu_Quentin_Vernaison
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public string[] mots = {};
        public string motMystere;
        public string motAffiche;
        public int erreurs = 0;
        public int maxErreurs = 7;
        public bool Premiergame = true;
        

        public string chemin = @"Ressource/Mots.txt";
        
        private PenduClass _penduClass;
       

        public MainWindow() // Initialise la fenêtre et lance la fonction StartNewGame
        {
            InitializeComponent();           
            LireFicher();
            _penduClass = new PenduClass(this);
            _penduClass.StartNewGame();
            Label LBL_Vie = new Label();
            
        }

        private void LireFicher()
        {
            mots = File.ReadAllLines(chemin);
        }


        private void RemplacerLettre(char lettre)
        {
            for (int i = 0; i < motMystere.Length; i++) //affiche la lettre dans le mot mystère si elle est dans le mot mystère et si elle n'est pas dans le mot mystère elle affiche un ?
            {
                if (motMystere[i] == lettre)
                {
                    motAffiche = motAffiche.Remove(i, 1).Insert(i, lettre.ToString());
                    TB_Mot.Text = motAffiche;
                }
            }
        }

        public void ChangementImage()
        {
            // Affiche l'image qui correspond au nombre d'erreurs
            PenduImages.Source = new BitmapImage(new Uri("Image/" + erreurs + ".png", UriKind.Relative));
            LBL_Vie.Content = "Vie: " + (maxErreurs - erreurs);
        }

        private void BTN_Click(object sender, RoutedEventArgs e) // Un bouton qui permet de choisir une lettre et qui vérifie si elle est dans le mot 
        {                                                        // mystère et qui l'a met en rouge si elle n'est pas dans le mot et en vert si elle est dans le mot

            Button bouton = (Button)sender;
            char lettre = Convert.ToChar(bouton.Content);

            if (Premiergame)
            {
                _penduClass.StartTimer();
                Premiergame = false;
            }


            bouton.IsEnabled = false;
            
            if (motMystere.Contains(lettre)) // Si la lettre est dans le mot mystère elle est affichée dans le mot à trouver et si elle n'est pas dans le mot mystère elle est affichée en rouge
            {
                bouton.Foreground = Brushes.Green;
                RemplacerLettre(lettre);
            }
            else if (!motMystere.Contains(lettre))
            {
                bouton.Foreground = Brushes.Red;
                erreurs++;
                ChangementImage();
            }

            if (motAffiche == motMystere) // Si le mot mystère est trouvé ouvre une popup avec un message de victoire
            {
                _penduClass.BloquerBouton();
                _penduClass.StopTimer();
                Window3 Window3 = new Window3();
                Window3.ShowDialog();
                this.Close();

            }

            if (erreurs == maxErreurs) // Si le mot n'est pas trouvé ouvre une popup avec un message de défaite et remet a 0 le timer
            {
                _penduClass.BloquerBouton();
                _penduClass.StopTimer();               
                Window4 Window4 = new Window4();
                Window4.ShowDialog();
                this.Close();
                Premiergame = true;
                
            }

              
            


        }
        // Une fonction pour restart avec le BTN_Restart_Click une partie  et qui réactive les boutons et reset l'image 

        private void BTN_Restart_Click(object sender, RoutedEventArgs e)
        {
            _penduClass.Restart();

        }

        private void BTN_Parametre_Click(object sender, RoutedEventArgs e) // Ouvre une popup avec les paramètres qui est sur window1.xaml , il s'ouvre qu'une seule fois a la fois 
        { 
            
            {
                Window1 Window1 = new Window1();
                Window1.ShowDialog();
            }

            

        }

        private void BTN_Aide_Click(object sender, RoutedEventArgs e)  // Permet d'avoir une lettre d'afficher dans le mot qui correspond a une lettre du mot mystère et fais perdre 2 vie 
        {
            _penduClass.Aide();


        }

        private void Btn_Hardcore_Click(object sender, RoutedEventArgs e) // Permet de jouer en hardcore et de ne pas voir le nombres de lettres
        {
            _penduClass.Hardcore();

        }
    }
}



