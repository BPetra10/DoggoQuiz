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

namespace Dogs.Learn
{
    /// <summary>
    /// Interaction logic for Learn.xaml
    /// </summary>
    public partial class Learn : Page
    {
        public Learn()
        {
            InitializeComponent();
        }

        private void ToNotePage() {
            Page notes = new Notes.Notes();
            Application.Current.MainWindow.Content = notes;
        }

        private void House_Click(object sender, RoutedEventArgs e)
        {
            Page wall = new WallOfGlory.WallOfGlory();
            Application.Current.MainWindow.Content = wall;
        }

        private void Komondor_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Agar_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void NemetJuhasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void FranciaBulldog_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Kuvasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Puli_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void ErdelyiKopo_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Mudi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Rottweiler_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Pumi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }

        private void Vizsla_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage();
        }
    }
}
