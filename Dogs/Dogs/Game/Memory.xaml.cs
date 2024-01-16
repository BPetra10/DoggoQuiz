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
using System.Windows.Threading;

namespace Dogs.Game
{
    /// <summary>
    /// Interaction logic for Memory.xaml
    /// </summary>
    public partial class Memory : Page
    {
        BitmapImage[] images = new BitmapImage[]
        {
            new BitmapImage(new Uri("memory/agar.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/francia_bulldog.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/kuvasz.png", UriKind.Relative)),
            new BitmapImage(new Uri("memory/mopdog.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/mudi.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/puli.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/roti_nemet.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("memory/vizsla.jpg", UriKind.Relative))
        };

        SolidColorBrush[] colors = new SolidColorBrush[]
        {
             new SolidColorBrush(Colors.DeepSkyBlue),
             new SolidColorBrush(Colors.Red),
             new SolidColorBrush(Colors.SpringGreen),
             new SolidColorBrush(Colors.Magenta),
             new SolidColorBrush(Colors.Gold),
             new SolidColorBrush(Colors.Indigo),
             new SolidColorBrush(Colors.Brown),
             new SolidColorBrush(Colors.Orchid)
        };

        byte[,] matrix;  //Number matrix for pairs

        //The buttons can be null, because the pairs are not always revealed, so they don't always have a value.
        Button? card1 = null;
        Button? card2 = null;

        //We will use this timer to hide cards
        DispatcherTimer timer = new DispatcherTimer();

        //We will use stopper to count how long it took to solve the memory game.
        DispatcherTimer stopwatch = new DispatcherTimer();

        //Store the actual date and time
        DateTime timeNow;

        int pairs = 0;

        int elapsedSec = 0;

        public Memory()
        {
            InitializeComponent();
            matrix = new byte[4, 4];
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += HideCard;
            stopwatch.Interval = TimeSpan.FromSeconds(0.5);
            stopwatch.Tick += PassedTime;
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            pairs = 0;
            Random r = new Random();
            List<byte> nums = new List<byte>() { };
            //Storing pairs nums: 1 1 2 2 3 3 ... 8 8
            for (byte i = 0; i < Math.Pow(4, 2) / 2; i++)
            {
                nums.Add(i);
                nums.Add(i);
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //Randomizing pairs place in matrix
                    int index = r.Next(0, nums.Count);
                    matrix[i, j] = nums[index];
                    nums.RemoveAt(index);

                    //Making buttons which we will add to our table grid
                    Button btn = new Button();
                    btn.Content = "?";
                    btn.Margin = new Thickness(5, 5, 5, 5);

                    //Adding Click event and unique button names to cards
                    btn.Click += RevealCard;
                    btn.Name = "b" + i + "_" + j; //eg. b0_0 will be the first btn

                    //Setting row and cols to btn and adding to table grid
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    table.Children.Add(btn);
                }
            }
            timeNow = DateTime.Now;
            stopwatch.Start();
        }

        private void RevealCard(object sender, RoutedEventArgs e)
        {
            //Get which card we clicked, and get its row and col value from its name, and getting it from matrix
            Button btn = (Button)sender;
            byte row = Convert.ToByte(btn.Name.Substring(1, 1));
            byte col = Convert.ToByte(btn.Name.Substring(3, 1));
            int element = matrix[row, col];

            //Adding color and img to btn
            Image img = new Image();
            img.Source = images[element];
            img.Stretch = Stretch.Fill;
            btn.Background = colors[element];
            btn.Padding = new Thickness(5, 5, 5, 5);
            btn.Content = img;

            if (card1 == null)
            {
                card1 = btn; //The first card was clicked now
                card1.IsHitTestVisible = false; //You cannot select the second card, as the same which you just clicked
            }
            else if (btn.Background == card1.Background) //If the 2 btn has the same background, we found a pair
            {
                btn.IsHitTestVisible = false;
                card1 = null; //The pair first element will be null, because we want to search for another pair
                pairs++;
                if (pairs == Math.Pow(4, 2) / 2)
                {
                    stopwatch.Stop();
                    //After you won, we set table and time to default
                    time.Content = "Eltelt idő: ";
                    table.Children.Clear();
                    SaveAndRedirect();
                }
            }
            else
            {
                //We can access card1 and card2 in HideCard evenet
                card2 = btn;
                table.IsHitTestVisible = false;
                timer.Start(); //This will hide card1 and 2 after 0.5 sec
            }

        }

        readonly Page shop = new WallOfGlory.WallOfGlory();
        
        void SaveAndRedirect()
        {
            DB.DB database = new DB.DB();
            int user_id = (int)Application.Current.Resources["UserId"];
            int points = 1000 / elapsedSec;

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
            MessageBox.Show("Szerzett tallérok: " + points.ToString());
            Application.Current.MainWindow.Content = shop;
        }

            /*if we don't have pairs, we want to set our cards to its default state:
            clickable card, default background and table.
            Stopping timer, and setting content clickable.
            */
            private void HideCard(object? sender, EventArgs e)
        {
            if (card1 != null)
            {
                card1.IsHitTestVisible = true;
                card1.ClearValue(BackgroundProperty);
                card1.Content = "?";
                card1 = null;
            }
            if (card2 != null)
            {
                card2.IsHitTestVisible = true;
                card2.ClearValue(BackgroundProperty);
                card2.Content = "?";
                card2 = null;
            }
            table.IsHitTestVisible = true;
            timer.Stop();
        }

        private void PassedTime(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan passed = now.Subtract(timeNow);
            //D2: Set leading zeros
            time.Content = "Eltelt idő: " + passed.Minutes.ToString("D2") + ":" + passed.Seconds.ToString("D2");
            elapsedSec = passed.Minutes*60 + passed.Seconds;
        }

        readonly Page learn = new Learn.Learn();
        private void ToLearn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }
    }
}
