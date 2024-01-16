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
    /// Interaction logic for QuizMain.xaml
    /// </summary>
    public partial class QuizMain : Page
    {
        public QuizMain()
        {
            InitializeComponent();
        }

        private List<CheckBox> GetCheckBoxesFromGridViewBoxes()
        {
            var GetGridViewBoxes = CheckBoxGrid.Children.OfType<Viewbox>().ToList();
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (var viewbox in GetGridViewBoxes)
            {
                checkBoxes.Add((CheckBox)viewbox.Child);
            }
            return checkBoxes;
        }
        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            var checkBoxes = GetCheckBoxesFromGridViewBoxes();
            var SelectedCheckBoxes = checkBoxes.Where(x => x.IsChecked == true).ToList();
            /*StringBuilder for selected dog names, 
             which we will give to Quiz.xaml constructor, 
            to use in database.getQuestions() parameter.*/
            StringBuilder checkedItems = new StringBuilder("", 120);

            if (SelectedCheckBoxes.Count == 1)
            {
                if (SelectedCheckBoxes[0].Content.ToString() == "összes")
                {
                    checkedItems.Append('*');
                    Page quiz = new Quiz(checkedItems.ToString());
                    Application.Current.MainWindow.Content = quiz;
                }
            }

            foreach (var checkbox in SelectedCheckBoxes)
            {
                if (checkbox.Content.ToString() != "összes")
                    checkedItems.Append("'" + checkbox.Content + "',");
            }

            if (checkedItems.Length > 0)
            {
                Page quiz = new Quiz(checkedItems.ToString().TrimEnd(','));
                Application.Current.MainWindow.Content = quiz;
            }
        }

        /*If you choose the checkbox with content összes, 
         then it will disable all the other checkbox,
         and remove their checkmark too.*/
        private void CheckUncheck(bool isTrue)
        {
            var checkBoxes = GetCheckBoxesFromGridViewBoxes();
            foreach (var checkbox in checkBoxes)
            {
                if (checkbox.Content.ToString() != "összes")
                {
                    checkbox.IsEnabled = isTrue;
                    checkbox.IsChecked = false;
                }
            }
        }
        private void all_Checked(object sender, RoutedEventArgs e)
        {
            CheckUncheck(false);
        }

        private void all_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckUncheck(true);
        }

        readonly Page learn = new Learn.Learn();
        private void BackToLearn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }
    }
}
