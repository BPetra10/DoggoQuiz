using Dogs.DB;
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
        List<Question> collection;
        public Quiz(string selectedDogs)
        {
            InitializeComponent();

            //Getting selected dogs questions and answers from DB, and Shuffling its sequence.
            DB.DB database = new DB.DB();
            collection = database.GetQuestions(selectedDogs);
            Shuffle(collection);
        }

        static Random rnd = new Random();
        static void Shuffle<T>(List<T> collection)
        {
            /*Fisher-Yates Shuffle Algorithm for randomizing.*/
            for (int i = collection.Count - 1; i > 0; --i)
            {
                int k = rnd.Next(i + 1);
                var temp = collection[i];
                collection[i] = collection[k];
                collection[k] = temp;
            }
        }
         void NextQuestionWithAns(int index) 
         {
            question.Text = collection[index].question;

            List<string> answers = new List<string>() { collection[index].answer1, collection[index].answer2, 
                collection[index].answer3, collection[index].correct };

            Shuffle(answers);

            ans1.Text = answers[0];
            ans2.Text = answers[1];
            ans3.Text = answers[2];
            ans4.Text = answers[3];
        }
        private void QuizGrid_Loaded(object sender, RoutedEventArgs e)
        {
            NextQuestionWithAns(0);
        }

        //TODO:Checking wich button has the correct answer maybe get ButtonList and check like that?
        /*
        private void CheckIfCorrect(object sender, Button senderBtn) {
            if (sender.Equals(senderBtn))
            {
                if (senderBtn.Content.ToString() == collection[i].correct)
                {
                    senderBtn.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    senderBtn.Background = new SolidColorBrush(Colors.Red);

                }
            }
        }
        */

        int i = 0;
        private void Btn(object sender, RoutedEventArgs e)
        {
            i++;
            if (i != collection.Count)
            {
                NextQuestionWithAns(i);
            }
            else {
                MessageBox.Show("Vége");
            }
        }
    }
}
