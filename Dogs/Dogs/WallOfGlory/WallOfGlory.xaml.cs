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
                    string uidOfRndImg = images[randNum].Uid;

                    database.ReOpenConn();
                    database.InsertOrUpdatePoints(user_id, userPoints.points - 100, false);

                    database.ReOpenConn();
                    var userImages = database.GetUserImages(user_id);

                    database.ReOpenConn();
                    if (userImages != null)
                    {
                        database.InsertOrUpdateImages(user_id, userImages.images + "," + uidOfRndImg, false);
                    }
                    else
                    {
                        database.InsertOrUpdateImages(user_id, uidOfRndImg, true);
                    }
                }
                else { Raffle.IsEnabled = false; }

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
                points.Text = "Pontjaid: " + userPoints.points.ToString();
                if (userPoints.points < 100)
                {
                    Raffle.IsEnabled = false;
                }
            }
        }
        private void LoadImages()
        {
            database.ReOpenConn();
            var allImage = database.GetUserImages(user_id);
            List<string> imgsSplit;
            //We get the user bought images, and we remove them from our local list, and making them visible in the shop.
            if (allImage != null)
            {
                imgsSplit = allImage.images.Split(",").OrderBy(x=>x).ToList();
                if (imgsSplit.Count != 11)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        for (int j = 0; j < imgsSplit.Count; j++)
                        {
                            if (images[i].Uid == imgsSplit[j])
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
                    /*Collapsing Raffle button, and changing raffleCongrats ViewBox columspan from 2 to 3
                     and changing text from raffletext to a congratulation text, and setting its color to crimson. */
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
