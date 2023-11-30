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

namespace Dogs.Notes
{
    /// <summary>
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : Page
    {
        public Notes()
        {
            InitializeComponent();
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = ((MainWindow)Application.Current.MainWindow).Main;
        }

        readonly Page learn = new Learn.Learn();
        private void BackToLearning_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn; 
        }

        readonly Page game = new Game.GameMain();
        private void ToGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = game;
        }
    }
}
