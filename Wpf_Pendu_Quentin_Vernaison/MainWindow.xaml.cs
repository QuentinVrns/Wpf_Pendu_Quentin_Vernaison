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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Pendu_Quentin_Vernaison
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string[] mots = { "INFORMATIQUE", "PROGRAMMATION", "MAISON", "AMBULANCE", "VOITURE" };
        private string motMystere;
        private string motAffiche;
        private int erreurs = 0;
        private int maxErreurs = 7;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            Random random = new Random();
            motMystere = mots[random.Next(mots.Length)];
            motAffiche = new string('_', motMystere.Length);
            TB_Mot.Text = motAffiche;
            erreurs = 0;

        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            char lettre = Convert.ToChar(bouton.Content);


            bouton.IsEnabled = false;
            if (motMystere.Contains(lettre))
            {
                for (int i = 0; i < motMystere.Length; i++)
                {
                    if (motMystere[i] == lettre)
                    {
                        motAffiche = motAffiche.Remove(i, 1).Insert(i, lettre.ToString());
                    }
                }
                TB_Mot.Text = motAffiche;
            }
            else
            {
                erreurs++;
                

                
            }

            if (motAffiche == motMystere)
            {
                
                MessageBox.Show("Bravo vous avez trouver le mot");
            }

            if (erreurs == maxErreurs)
            {
                
                MessageBox.Show("Vous avez perdu");
            }
        }

        private void BTN_Restart_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}


