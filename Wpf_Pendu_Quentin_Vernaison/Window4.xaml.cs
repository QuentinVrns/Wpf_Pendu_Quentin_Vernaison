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

namespace Wpf_Pendu_Quentin_Vernaison
{
    /// <summary>
    /// Logique d'interaction pour Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)  // Bouton qui permet de retourner au menu principal et empecher de pouvoir clicker sur la croix pour fermer la fenêtre
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            
        }

        private void Grid_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
