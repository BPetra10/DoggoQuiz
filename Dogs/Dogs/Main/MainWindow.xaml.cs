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

namespace Dogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }

        readonly Page login = new Login_Register.Login();
        readonly Page learn = new Learn.Learn();
        private void Log_Click(object sender, RoutedEventArgs e)
        {
            if(Application.Current.Resources["UserId"]==null)
                Content = login;
            else
                Content = learn;
        }

        readonly Page description = new Description.Description();
        private void Description_Click(object sender, RoutedEventArgs e)
        {
            Content = description;
        }

        readonly Page nav = new Navigation.Navigation();
        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            Content = nav;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
