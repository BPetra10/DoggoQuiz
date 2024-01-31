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

namespace Dogs.Result
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Page
    {
        public Result()
        {
            InitializeComponent();
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = ((MainWindow)Application.Current.MainWindow).Main;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DB.DB db = new DB.DB();
            var scores = db.GetScores();
            if (scores.Count != 0)
            {
                for (int i = 0; i < scores.Count; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        Viewbox vb = new Viewbox();
                        TextBlock tb = new TextBlock();
                        if (j == 0)
                            tb.Text = scores[i].username;
                        else if (j == 1)
                            tb.Text = scores[i].images.ToString();
                        else if (j == 2)
                            tb.Text = scores[i].points.ToString();
                        vb.Child = tb;
                        vb.Margin = new Thickness(10);
                        Grid.SetRow(vb, i + 2);
                        Grid.SetColumn(vb, j + 2);
                        scoreboard.Children.Add(vb);
                    }
                }
            }
        }
    }
}
