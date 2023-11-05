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
            if (questionIndex != collection.Count)
            {
                ans1.Foreground = new SolidColorBrush(Colors.Black);
                ans2.Foreground = new SolidColorBrush(Colors.Black);
                ans3.Foreground = new SolidColorBrush(Colors.Black);
                ans4.Foreground = new SolidColorBrush(Colors.Black);

                question.Text = collection[index].question;

                List<string> answers = new List<string>() { collection[index].answer1, collection[index].answer2,
                collection[index].answer3, collection[index].correct };

                Shuffle(answers);

                ans1.Text = answers[0];
                ans2.Text = answers[1];
                ans3.Text = answers[2];
                ans4.Text = answers[3];
            }
        }
        private void QuizGrid_Loaded(object sender, RoutedEventArgs e)
        {
            NextQuestionWithAns(0);
        }
        
        int  questionIndex = 0;
        //TODO: how to color sender if it was 
        private void Btn(object sender, RoutedEventArgs e)
        {
            List<TextBlock> answerList = new List<TextBlock>() {ans1,ans2,ans3,ans4};

            Button senderBtn = (Button)sender;
            
            if (questionIndex != collection.Count)
            {
                if (senderBtn.Content.ToString() == collection[questionIndex].correct)
                {
                    senderBtn.Foreground = new SolidColorBrush(Colors.Green);
                }
                else 
                {
                    var a = answerList.Where(x => x.Text == collection[questionIndex].correct).Single();
                    a.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            else {
                MessageBox.Show("Vége");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (questionIndex != collection.Count)
            {
                questionIndex++;
                NextQuestionWithAns(questionIndex);
            }
            else {
                MessageBox.Show("Vége");
            }
        }
    }
}
