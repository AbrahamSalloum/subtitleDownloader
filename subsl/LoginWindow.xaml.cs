using subsl.Models;
using subsl.Services;
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
using System.Windows.Shapes;

namespace subsl
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this; 
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            string apikey = ConsumerKeyBox.Text;

            LoginInput.username = username;
            LoginInput.password = password;
            LoginInput.apikey = apikey;
            this.Close(); 

        }
    }
}
