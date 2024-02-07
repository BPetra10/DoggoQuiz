using Dogs.DB;
using Dogs.Learn;
using Dogs.Login_Register;
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

namespace Dogs.Game
{
    /// <summary>
    /// Interaction logic for GameMain.xaml
    /// </summary>
    public partial class GameMain : Page
    {
        public GameMain()
        {
            InitializeComponent();
        }

        readonly Page memory = new Memory();
        private void ToMemory_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = memory;
        }

        readonly Page quizMain = new QuizMain(); 
        private void ToQuiz_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = quizMain;
        }

        readonly Page learn = new Learn.Learn();
        private void BackToLearn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = ((MainWindow)Application.Current.MainWindow).Main;
        }

        readonly Page catchItem = new Catch();
        private void ToCatch_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = catchItem;
        }
    }
}
