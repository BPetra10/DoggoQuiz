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

        private void ToNotePage(string Dogname)
        {  
            var notes = new Notes.Notes();
            notes.dogName.Text = Dogname;
            Application.Current.MainWindow.Content = notes;
        }

        private void House_Click(object sender, RoutedEventArgs e)
        {
            Page wall = new WallOfGlory.WallOfGlory();
            Application.Current.MainWindow.Content = wall;
        }

        private void Komondor_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Komondor");
        }

        private void Agar_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Agár");
        }

        private void NemetJuhasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Németjuhász");
        }

        private void FranciaBulldog_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Francia bulldog");
        }

        private void Kuvasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Kuvasz");
        }

        private void Puli_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Puli");
        }

        private void ErdelyiKopo_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Erdelyi kopó");
        }

        private void Mudi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Mudi");
        }

        private void Rottweiler_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Rottweiler");
        }

        private void Pumi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Pumi");

        }

        private void Vizsla_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("Vizsla");
        }
    }
}
