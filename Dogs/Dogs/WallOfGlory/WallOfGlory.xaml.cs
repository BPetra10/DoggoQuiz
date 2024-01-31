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

namespace Dogs.WallOfGlory
{
    /// <summary>
    /// Interaction logic for WallOfGlory.xaml
    /// </summary>
    public partial class WallOfGlory : Page
    {
        private List<Image> images;
        DB.DB database = new DB.DB();
        int user_id = (int)Application.Current.Resources["UserId"];
        public WallOfGlory()
        {
            InitializeComponent();
            images = new List<Image>() { agar, kopo, bulldog, komondor, kuvasz, mudi, njuhasz, puli, pumi, roti, vizsla };
        }

        readonly Page learn = new Learn.Learn();
        private void BackToLearn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = learn;
        }

        private void Raffle_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            database.ReOpenConn();
            var userPoints = database.GetUserPoint(user_id);

            if (userPoints != null)
            {
                if (images.Count > 0 && userPoints.points >= 100)
                {
                    //Randomizing which picture the user will get
                    var randNum = rnd.Next(0, images.Count);
                    images[randNum].Visibility = Visibility.Visible;
                    //We have to get the unique identifier of the image, because we will have to store which picture the user have 
                    int uidOfRndImg = Int32.Parse(images[randNum].Uid);

                    database.ReOpenConn();
                    database.InsertOrUpdatePoints(user_id, userPoints.points - 100, false);

                    database.ReOpenConn();
                    database.InsertImages(user_id, uidOfRndImg);
                }

                //Showing actual point and images after buying something:
                database.ReOpenConn();
                userPoints = database.GetUserPoint(user_id);
                LoadPoints(userPoints);
                LoadImages();
            }
        }
        private void WallGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var userPoints = database.GetUserPoint(user_id);
            LoadPoints(userPoints);
            LoadImages();
        }

        private void LoadPoints(Points? userPoints) 
        {
            if (userPoints != null)
            {
                points.Text = "Tallér: " + userPoints.points.ToString();
            }
        }
        private void LoadImages()
        {
            database.ReOpenConn();
            var allImage = database.GetUserImages(user_id);

            allImage = allImage.OrderBy(x=>x.images).ToList();

            //We get the user bought images, and we remove them from our local list, and making them visible in the shop.
            if (allImage != null)
            {
                if (allImage.Count != 11)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        for (int j = 0; j < allImage.Count; j++)
                        {
                            if (Int32.Parse(images[i].Uid) == allImage[j].images)
                            {
                                images[i].Visibility = Visibility.Visible;
                                images.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    images.ForEach(x => x.Visibility = Visibility.Visible);
                    //Collapsing Raffle button, and changing raffleCongrats ViewBox columspan from 2 to 3
                    // and changing text from raffletext to a congratulation text, and setting its color to crimson.
                    Raffle.Visibility = Visibility.Collapsed;
                    Grid.SetColumnSpan(raffleCongrats,3);
                    var text = (TextBlock)raffleCongrats.Child;
                    text.Text = "Gratulálok, mindent kigyűjtöttél!";
                    text.Foreground = new SolidColorBrush(Colors.Crimson);
                }
            }
        }

    }
}
