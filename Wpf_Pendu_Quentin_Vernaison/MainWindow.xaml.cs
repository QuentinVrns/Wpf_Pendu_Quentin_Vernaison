using System;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Wpf_Pendu_Quentin_Vernaison
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string[] mots = { "INFORMATIQUE", "PROGRAMMATION", "MAISON", "AMBULANCE", "VOITURE", "AVION", "PARIS", "ANNECY", "VOYAGE", "VACANCE" , "ETUDIANT", "ECOLE", "NOURRITURE" };
        private string motMystere;
        private string motAffiche;
        private int erreurs = 0;
        private int maxErreurs = 7;
        private bool Premiergame = true;
        DispatcherTimer timer;

        
        
       

        public MainWindow() // Initialise la fenêtre et lance la fonction StartNewGame
        {
            InitializeComponent();
            StartNewGame();
            Label LBL_Vie = new Label();          

        }

        private void StartNewGame()   // Une fonction qui permet de commencer une nouvelle partie et qui choisi un mot aléatoire dans le tableau de mots et qui affiche le mot mystère en ?
        {
            Random random = new Random();
            motMystere = mots[random.Next(mots.Length)];
            motAffiche = new string('?', motMystere.Length);
            TB_Mot.Text = motAffiche;
            erreurs = 0;

        }

        private void BTN_Click(object sender, RoutedEventArgs e) // Un bouton qui permet de choisir une lettre et qui vérifie si elle est dans le mot 
        {                                                        // mystère et qui l'a met en rouge si elle n'est pas dans le mot et en vert si elle est dans le mot

            Button bouton = (Button)sender;
            char lettre = Convert.ToChar(bouton.Content);

            if (Premiergame)
            {
                StartTimer();
                Premiergame = false;
            }


            bouton.IsEnabled = false;
            


            for (int i = 0; i < motMystere.Length; i++) //affiche la lettre dans le mot mystère si elle est dans le mot mystère et si elle n'est pas dans le mot mystère elle affiche un ?
            {
                if (motMystere[i] == lettre)
                {
                    motAffiche = motAffiche.Remove(i, 1).Insert(i, lettre.ToString());
                    TB_Mot.Text = motAffiche;
                }
            }
            if (motMystere.Contains(lettre)) // Si la lettre est dans le mot mystère elle est affichée dans le mot à trouver et si elle n'est pas dans le mot mystère elle est affichée en rouge
            {
                bouton.Foreground = Brushes.Green;
            }
            else if (!motMystere.Contains(lettre))
            {
                bouton.Foreground = Brushes.Red;


                erreurs++;
                // Affiche l'image qui correspond au nombre d'erreurs
                PenduImages.Source = new BitmapImage(new Uri("Image/" + erreurs + ".png", UriKind.Relative));
                LBL_Vie.Content = "Vie: " + (maxErreurs - erreurs);

            }

            if (motAffiche == motMystere) // Si le mot mystère est trouvé ouvre une popup avec un message de victoire
            {
                Window3 Window3 = new Window3();
                Window3.ShowDialog();

            }


            if (erreurs == maxErreurs) // Si le mot est trouvé ouvre une popup avec un message de défaite
            {
                Window4 Window4 = new Window4();
                Window4.ShowDialog();
            }
            // Si le timer est à 0 ouvre une popup avec un message de défaite

         
        }
        // Une fonction pour restart avec le BTN_Restart_Click une partie  et qui réactive les boutons et reset l'image 

        private void BTN_Restart_Click(object sender, RoutedEventArgs e)
        {
            PenduImages.Source = new BitmapImage(new Uri("Image/0.png", UriKind.Relative));
            StopTimer();
            StartNewGame();
            LBL_Vie.Content = "Vie: " + (maxErreurs - erreurs);
            foreach (var bouton in Grille.Children.OfType<Button>()) // Réactive les boutons et remet la couleur des boutons avec la couleur initial
            {
                bouton.IsEnabled = true;
                bouton.Foreground = Brushes.Black;

            }
            Premiergame = true;

        }

        private void BTN_Parametre_Click(object sender, RoutedEventArgs e) // Ouvre une popup avec les paramètres qui est sur window1.xaml
        {
            Window1 Window1 = new Window1();
            Window1.ShowDialog();
        }




        private void BTN_Aide_Click(object sender, RoutedEventArgs e)  // Permet d'avoir une lettre d'afficher dans le mot qui correspond a une lettre du mot mystère et fais perdre 2 vie 
        {
            Random random = new Random();
            int index = random.Next(motMystere.Length);
            char lettre = motMystere[index];
            motAffiche = motAffiche.Remove(index, 1).Insert(index, lettre.ToString());
            TB_Mot.Text = motAffiche;
            erreurs = erreurs + 2;
            PenduImages.Source = new BitmapImage(new Uri("Image/" + erreurs + ".png", UriKind.Relative));
            BTN_Aide.IsEnabled = false;
            LBL_Vie.Content = "Vie: " + (maxErreurs - erreurs);
            // Met la lettre utiliser en vert
            foreach (var bouton in Grille.Children.OfType<Button>())
            {
                if (bouton.Content.ToString() == lettre.ToString())
                {
                    bouton.Foreground = Brushes.Green;
                    bouton.IsEnabled = false;
                }
            }            
                               
              
        }

        // code moi un timer qui met a jour la progress bar jusqu'au maximum
        // quand la progress bar est au maximum, tu perds la partie et la progress bar est remise à 0
        private void StartTimer()
        {
            Bar.Value = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // code moi une function qui stop le timer
        private void StopTimer()
        {
            Bar.Value = 0;
            timer.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Bar.Value += 1;
            if (Bar.Value == Bar.Maximum)
            {
                
                
                Window4 Window4 = new Window4();
                Window4.ShowDialog();


                
                Premiergame = true;
                StopTimer();
            }
        }


        


    }
}



