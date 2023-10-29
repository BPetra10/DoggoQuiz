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

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0){
                errorMsg.Text = "Nem adtál meg felhasználónevet!";
                username.Focus();
            }
            else if (username.Text.Length < 6){
                errorMsg.Text = "A felhasználónév legalább 6 betű!";
                username.Focus();
            }
            else{
                string usernameText = username.Text;
                if (password.Password.Length == 0) {
                    errorMsg.Text = "Nem adtál meg jelszót!";
                    password.Focus();
                }
                else if (password2.Password.Length == 0) {
                    errorMsg.Text = "Nem adtad meg újra a jelszót!";
                    password2.Focus();
                }
                else if (password.Password != password2.Password) {
                    errorMsg.Text = "A 2 jelszó nem egyezik meg!";
                }
                else{
                    Regex validation = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                    if (!validation.IsMatch(password.Password)){
                        errorMsg.Text = "A jelszó legalább 8 karakteres, kis és nagybetűt, számot, speciális karaktert tartalmazó!";
                        password.Focus();
                    }
                    else {
                        Page login = new Login();
                        Application.Current.MainWindow.Content = login;
                    }
                }
            }
        }

        private void ToLogin_Click(object sender, RoutedEventArgs e)
        {
            Page login = new Login();
            Application.Current.MainWindow.Content = login;
        }
    }
}
