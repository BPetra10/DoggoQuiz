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

namespace Dogs.Login_Register
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        readonly Page learn = new Learn.Learn();
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0)
            {
                errorMsg.Text = "Nem adtál meg felhasználónevet!";
                username.Focus();
            }
            else if (password.Password.Length == 0)
            {
                errorMsg.Text = "Nem adtál meg jelszót!";
            }
            else {
                PasswordHasher passwordHasher = new PasswordHasher();
                DB.DB database = new DB.DB();
                if (database.CheckIfUserExist(username.Text))
                {
                    database.ReOpenConn();
                    var user = database.GetUserSaltAndPwd(username.Text);
                    if (user!=null)
                    {
                        if (passwordHasher.IsValid(password.Password, user.password, Convert.FromHexString(user.salt)))
                        {
                            /*If we have a successful login, we add the userId to Appplication.Current.Resources
                             so we can access the logged-in person ID later in the app.*/
                            database.ReOpenConn();
                           
                            int id = database.GetUserId(username.Text);
                            if (id != 0)
                            {
                                Application.Current.Resources.Add("UserId", id);
                                Application.Current.MainWindow.Content = learn;
                            }
                        }
                        else
                        {
                            errorMsg.Text = "Nem megfelelő felhasználónév vagy jelszó!";
                        }
                    }
                }
                else {
                    errorMsg.Text = "Ez a felhasználó nem létezik az adatbázisban!";
                }
            }
        }

        private void ToRegister_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Register();
        }
    }
}
