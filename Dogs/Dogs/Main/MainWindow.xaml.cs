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
        private void Learn_Click(object sender, RoutedEventArgs e)
        {
            Page registration = new Login_Register.Register();
            Content = registration;
        }
        private void Description_Click(object sender, RoutedEventArgs e)
        {
            Page description = new Description.Description();
            Content = description;
        }

        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            Page nav = new Navigation.Navigation();
            Content = nav;
        }
    }
}
