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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Dogs.Game
{
    /// <summary>
    /// Interaction logic for Catch.xaml
    /// </summary>
    public partial class Catch : Page
    {
        int maxItems = 5;
        int currentItems = 0;

        int points = 0;
        int missed = 0;

        DispatcherTimer gameTimer = new DispatcherTimer();

        //How much lower the element will be on the canvas at every interval/tick
        int itemSpeed = 10;

        /*This will store the bone/ball which we catched or which fell down,
         because we have to remove these items from the canvas. */
        List<Rectangle> RemoveItems = new List<Rectangle>();

        Rect dogHitBox;

        ImageBrush dogImage = new ImageBrush();

        public Catch()
        {
            InitializeComponent();
        }

        private void Game(object? sender, EventArgs e)
        {
            scorelbl.Content = "Elkapott: " + points;
            missedlbl.Content = "Elvétett: " + missed;

            if (currentItems < maxItems)
            {
                MakeBallOrBone();
                currentItems++;
                RemoveItems.Clear();
            }


            foreach (var x in Field.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "falls")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + itemSpeed);

                    Rect itemsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    dogHitBox = new Rect(Canvas.GetLeft(dog), Canvas.GetTop(dog), dog.Width, dog.Height);

                    if (dogHitBox.IntersectsWith(itemsHitBox))
                    {
                        RemoveItems.Add(x);
                        currentItems--;
                        points++;
                    }

                    if (Canvas.GetTop(x) > Field.ActualHeight)
                    {
                        RemoveItems.Add(x);
                        currentItems--;
                        missed++;
                    }
                }
            }

            foreach (var i in RemoveItems)
            {
                Field.Children.Remove(i);
            }

            if (points > 10 && points < 40)
            {
                itemSpeed = 15;
            }
            else if (points > 40 && points < 60)
            {
                itemSpeed = 20;
            }
            else if (points > 60)
            {
                itemSpeed = 30;
            }

            if (missed > 10)
            {
                gameTimer.Stop();

                DB.DB database = new DB.DB();
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

                MessageBoxResult result = MessageBox.Show("Szeretnél újra játszani?" + Environment.NewLine + 
                    "Tallérok száma: " +points, "Kérdés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    Page shop = new WallOfGlory.WallOfGlory();
                    Application.Current.MainWindow.Content = shop;
                }
            }
        }

        private void Field_MouseMove(object sender, MouseEventArgs e)
        {
            //We want to know where our mouse/pointer is in the canvas:
            Point position = e.GetPosition(this);

            double pX = position.X;

            Canvas.SetLeft(dog, pX - 30);

            if (Canvas.GetLeft(dog) < Field.ActualWidth / 2)
            {
                dogImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/dogleft.png"));
            }
            else
            {
                dogImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/dogright.png"));
            }

        }

        private void ResetGame()
        {
            Field.Children.Clear();
            maxItems = 5;
            currentItems = 0;
            points = 0;
            missed = 0;
            itemSpeed = 10;
            scorelbl.Content = "Elkapott: 0";
            missedlbl.Content = "Elvétett: 0";
            Canvas.SetTop(dog, Field.ActualHeight - 100);
            Field.Children.Add(dog);

            gameTimer.Tick -= Game;
            gameTimer.Tick += Game;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }

        Random rnd = new Random();
        private void MakeBallOrBone()
        {

            ImageBrush item = new ImageBrush();

            int i = rnd.Next(1, 4);

            switch (i)
            {
                case 1:
                    item.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/ball.png"));
                    break;
                case 2:
                    item.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/ball2.png"));
                    break;
                case 3:
                    item.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/bone.png"));
                    break;
                case 4:
                    item.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/bone2.png"));
                    break;

            }

            Rectangle itemBox = new Rectangle
            {
                Tag = "falls",
                Width = 60,
                Height = 60,
                Fill = item
            };
            //Set the bone/ball position randomly in canvas:
            /*setleft will position the item between 10 and the field size-60,
            where 60 is the item size*/
            Canvas.SetLeft(itemBox, rnd.Next(10, (int)Field.ActualWidth - 60));
            /*-1 is important, because we want to start the objects falling 
             from outside of the canvas. 
             So if i get randomly 65, the item will start to fall from -65;*/
            Canvas.SetTop(itemBox, -1 * rnd.Next(60, (int)Field.ActualHeight));

            Field.Children.Add(itemBox);

        }
        private void Field_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetTop(dog, Field.ActualHeight - 100);
            dogImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Game/catch/dogleft.png"));
            dog.Fill = dogImage;
        }

        

        readonly Page learn = new Learn.Learn();
        private void BackToLearn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }

        private void GameRestart_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Field.Focus();

            gameTimer.Tick += Game;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);
            gameTimer.Start();
        }
    }
}
