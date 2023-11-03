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
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        string selectedDogs;
        public Quiz(string selectedDogs)
        {
            InitializeComponent();
            this.selectedDogs = selectedDogs;
        }

        private void QuizGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DB.DB database = new DB.DB();
            var collection = database.GetQuestions(selectedDogs);
            foreach (var item in collection)
            {
                test.Content = test.Content + "\n" + item.question;
            }
        }
    }
}
