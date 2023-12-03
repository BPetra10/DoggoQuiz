using Dogs.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        readonly Page login = new Login();
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0){
                errorMsg.Text = "Nem adtál meg felhasználónevet!";
                username.Focus();
            }
            else if (username.Text.Length!=0){
                Regex uservalid = new Regex("^[a-záéúőóüöíA-ZÁÉÚŐÓÜÖÍ0-9]{6,12}$");
                if (!uservalid.IsMatch(username.Text))
                {
                    errorMsg.Text = "A felhasználónév legalább 6, max 12 karakter, betűvel kezdődik és számot is tartalmazhat!";
                    username.Focus();
                }
                else {
                    if (password.Password.Length == 0)
                    {
                        errorMsg.Text = "Nem adtál meg jelszót!";
                        password.Focus();
                    }
                    else if (password2.Password.Length == 0)
                    {
                        errorMsg.Text = "Nem adtad meg újra a jelszót!";
                        password2.Focus();
                    }
                    else if (password.Password != password2.Password)
                    {
                        errorMsg.Text = "A 2 jelszó nem egyezik meg!";
                    }
                    else
                    {
                        Regex pwdvalid = new Regex("^(?=.*?[A-ZÁÉÚŐÓÜÖÍ])(?=.*?[a-záéúőóüöí])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$");
                        if (!pwdvalid.IsMatch(password.Password))
                        {
                            errorMsg.Text = "A jelszó legalább 8, de max 16 karakteres, kis és nagybetűt, számot, speciális karaktert tartalmazó!";
                            password.Focus();
                        }
                        else
                        {
                            var database = new DB.DB();
                            if (!database.CheckIfUserExist(username.Text))
                            {
                                database.ReOpenConn();

                                PasswordHasher passwordHasher = new PasswordHasher();
                                var hashedpass = passwordHasher.Generate(password.Password, out var salt);
                                database.InsertUser(username.Text, hashedpass, Convert.ToHexString(salt));

                                Application.Current.MainWindow.Content = login;
                            }
                            else
                            {
                                errorMsg.Text = "Ez a felhasználó már létezik!";
                            }
                        }
                    }
                }
            }
        }

        private void ToLogin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = login;
        }

        private void errorMsg_MouseEnter(object sender, MouseEventArgs e)
        {
            help.Visibility = Visibility.Visible;
        }

        private void errorMsg_MouseLeave(object sender, MouseEventArgs e)
        {
            help.Visibility = Visibility.Hidden;
        }
    }
}
