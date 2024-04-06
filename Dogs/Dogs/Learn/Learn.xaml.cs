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
            
            var database = new DB.DB();
            var notesList = database.GetNotes(Dogname);

           for (int i = 0; i < notesList.Count; i++)
            {
                string fieldName = "tb" + (i+1);
                var myTextBlock = (TextBlock)notes.FindName(fieldName);
                myTextBlock.Text = notesList[i].LearningNotes;
            }
            
            Application.Current.MainWindow.Content = notes;
        }

        private void House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new WallOfGlory.WallOfGlory();
        }

        private void Komondor_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("komondor");
        }

        private void Agar_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("agár");
        }

        private void NemetJuhasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("németjuhász");
        }

        private void FranciaBulldog_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("francia bulldog");
        }

        private void Kuvasz_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("kuvasz");
        }

        private void Puli_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("puli");
        }

        private void ErdelyiKopo_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("erdélyi kopó");
        }

        private void Mudi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("mudi");
        }

        private void Rottweiler_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("rottweiler");
        }

        private void Pumi_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("pumi");
        }

        private void Vizsla_Click(object sender, RoutedEventArgs e)
        {
            ToNotePage("vizsla");
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = ((MainWindow)Application.Current.MainWindow).Main;
        }
    }
}
