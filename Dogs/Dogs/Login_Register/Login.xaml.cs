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
                DB.PasswordHasher passwordHasher = new DB.PasswordHasher();
                DB.DB database = new DB.DB();
                if (database.CheckIfUserExist(username.Text))
                {
                    database.ReOpenConn();
                    var user = database.GetUserSaltAndPwd(username.Text);
                    if (user!=null)
                    {
                        if (passwordHasher.IsValid(password.Password, user.password, Convert.FromHexString(user.salt)))
                        {
                            Page learn = new Learn.Learn();
                            Application.Current.MainWindow.Content = learn;
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
            Page register = new Register();
            Application.Current.MainWindow.Content = register;
        }
    }
}
