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
        public WallOfGlory()
        {
            InitializeComponent();
            images = new List<Image>() { agar, kopo, bulldog, komondor, kuvasz, mudi, njuhasz, puli, pumi, roti, vizsla };
        }

        private void BackToLearn_Click(object sender, RoutedEventArgs e)
        {
            Page learn = new Learn.Learn();
            Application.Current.MainWindow.Content = learn;
        }

        private void Raffle_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (images.Count > 0)
            {
                var randNum = rnd.Next(0, images.Count);
                images[randNum].Visibility = Visibility.Visible;
                images.Remove(images[randNum]);
            }
            else {
                MessageBox.Show("Kigyűjtötted a képeket!");
            }
        }

        private void WallGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DB.DB database = new DB.DB();
            int user_id = (int)Application.Current.Resources["UserId"];
            var userPoints = database.GetUserPoint(user_id);
            if (userPoints != null)
            {
                points.Text = "Pontjaid: " + userPoints.points.ToString();
            }
        }
    }
}
