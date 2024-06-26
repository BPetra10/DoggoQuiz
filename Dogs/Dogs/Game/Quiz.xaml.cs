﻿using Dogs.DB;
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
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace Dogs.Game
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        List<Question> collection;
        DB.DB database = new DB.DB();
        
        int removeCounter = 3;

        List<TextBlock> answerList;
        public Quiz(string selectedDogs)
        {
            InitializeComponent();

            //Getting selected dogs questions and answers from DB, and Shuffling its sequence.
            collection = database.GetQuestions(selectedDogs);
            Shuffle(collection);

            answerList = new List<TextBlock>() { ans1, ans2, ans3, ans4 };
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

        readonly Page shop = new WallOfGlory.WallOfGlory();
        void SaveAndRedirect() {
            database.ReOpenConn();
            int user_id = (int)Application.Current.Resources["UserId"];
            var userPoints = database.GetUserPoint(user_id);
            if (userPoints != null)
            {
                //Update method for DB: getting databse points and adding local points to it.
                database.ReOpenConn();
                database.InsertOrUpdatePoints(user_id, userPoints.points + points, false);
            }
            else
            {
                //Insert into points table
                database.ReOpenConn();
                database.InsertOrUpdatePoints(user_id, points, true);
            }
            //Show actualpoints in messagebox and go to shop.
            MessageBox.Show("Helyes válaszok száma: "+collection.Count+"/"+points/10);
            Application.Current.MainWindow.Content = shop;
        }

        int questionIndex = 0;
        void NextQuestionWithAns(int questionIndex) 
        {
            if (questionIndex != collection.Count)
            {
                Btn1.IsHitTestVisible = Btn2.IsHitTestVisible = Btn3.IsHitTestVisible = Btn4.IsHitTestVisible = Help.IsHitTestVisible = true;
                ans1.Foreground = new SolidColorBrush(Colors.Black);
                ans2.Foreground = new SolidColorBrush(Colors.Black);
                ans3.Foreground = new SolidColorBrush(Colors.Black);
                ans4.Foreground = new SolidColorBrush(Colors.Black);

                question.Text = collection[questionIndex].question;

                List<string> answers = new List<string>() { collection[questionIndex].answer1, 
                collection[questionIndex].answer2, collection[questionIndex].answer3, collection[questionIndex].correct };

                Shuffle(answers);

                ans1.Text = answers[0];
                ans2.Text = answers[1];
                ans3.Text = answers[2];
                ans4.Text = answers[3];
            }
            else 
            {
                SaveAndRedirect();
            }
        }
        private void QuizGrid_Loaded(object sender, RoutedEventArgs e)
        {
            NextQuestionWithAns(0);
        }

        //Change the question automatically after 3 sec.
        void DelayQuestion()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            
            timer.Tick += delegate
            {
                NextQuestionWithAns(questionIndex);
                timer.Stop();
            };
           
            timer.Start();
        }

        int points = 0;
        bool ansClicked = false;
        private void Btn(object sender, RoutedEventArgs e)
        {
            answerList = new List<TextBlock>() { ans1, ans2, ans3, ans4 };
            //Disable all the buttons, so the user cannot click over-and-over again to select some answer 
            Btn1.IsHitTestVisible = Btn2.IsHitTestVisible = Btn3.IsHitTestVisible = Btn4.IsHitTestVisible = Help.IsHitTestVisible = false;

            //We know that the sender is a Button, and this button always has a viewbox inside.
            //Inside the viewbox, there is only one child always, and that is a TextBlock.
            Button senderBtn = (Button)sender;
            Viewbox senderBtnVB = (Viewbox)senderBtn.Content;
            TextBlock senderBtnTB = (TextBlock)senderBtnVB.Child;
            
            if (questionIndex != collection.Count)
            {
                if (senderBtnTB.Text == collection[questionIndex].correct)
                {
                    points += 10;
                    scoreText.Text = "Megszerzett tallérok: " + points;
                    senderBtnTB.Foreground = new SolidColorBrush(Colors.Green);
                }
                else 
                {
                    senderBtnTB.Foreground = new SolidColorBrush(Colors.Red);
                    var correctAns = answerList.Where(x => x.Text == collection[questionIndex].correct).Single();
                    correctAns.Foreground = new SolidColorBrush(Colors.Green);
                }
                questionIndex++;
                DelayQuestion();
                ansClicked = true;
            }
        }

        /*The user will get 3 help, which they can use in questions:
         They can use all the help in only one question, basically skipping that,
         or just use one help in one question. 
         */ 
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            if (removeCounter != 0)
            {
                Viewbox Btn1VB = (Viewbox)Btn1.Content;
                TextBlock Btn1TB = (TextBlock)Btn1VB.Child;
                Viewbox Btn2VB = (Viewbox)Btn2.Content;
                TextBlock Btn2TB = (TextBlock)Btn2VB.Child;
                Viewbox Btn3VB = (Viewbox)Btn3.Content;
                TextBlock Btn3TB = (TextBlock)Btn3VB.Child;
                Viewbox Btn4VB = (Viewbox)Btn4.Content;
                TextBlock Btn4TB = (TextBlock)Btn4VB.Child;

                if (ansClicked == false)
                {
                    for (int i = 0; i < answerList.Count; i++)
                    {
                        if (answerList[i].Text == collection[questionIndex].correct)
                        {
                            answerList.RemoveAt(i);
                        }
                    }

                    int removeBtn = rnd.Next(0, answerList.Count);

                    if (answerList[removeBtn] == Btn1TB)
                    {
                        ans1.Foreground = new SolidColorBrush(Colors.DarkRed);
                        Btn1.IsHitTestVisible = false;
                        answerList.RemoveAt(removeBtn);
                        removeCounter--;
                    }
                    else if (answerList[removeBtn] == Btn2TB)
                    {
                        ans2.Foreground = new SolidColorBrush(Colors.DarkRed);
                        Btn2.IsHitTestVisible = false;
                        answerList.RemoveAt(removeBtn);
                        removeCounter--;
                    }
                    else if (answerList[removeBtn] == Btn3TB)
                    {
                        ans3.Foreground = new SolidColorBrush(Colors.DarkRed);
                        Btn3.IsHitTestVisible = false;
                        answerList.RemoveAt(removeBtn);
                        removeCounter--;
                    }
                    else if (answerList[removeBtn] == Btn4TB)
                    {
                        ans4.Foreground = new SolidColorBrush(Colors.DarkRed);
                        Btn4.IsHitTestVisible = false;
                        answerList.RemoveAt(removeBtn);
                        removeCounter--;
                    }
                }
                else
                {
                    answerList = new List<TextBlock>() { ans1, ans2, ans3, ans4 };
                    ansClicked = false;
                }
            }
            else {
                MessageBox.Show("Elfogytak a segítségek!");
                Help.IsHitTestVisible = false;
            }
        }

        readonly Page learn = new Learn.Learn();
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }
    }
}
