using AuthInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthenticatorInterface server;
        UserModel user;


        public MainWindow()
        {
            InitializeComponent();
            
            
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost/AuthenticationService";
            var channelFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            server = channelFactory.CreateChannel();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usrNameBox.Text.Trim();
            string password = passwordBox.Password.Trim();

          
            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            int token = server.Login(username, password);

            if (token != 0)
            {
                user = new UserModel(username, token);
                MessageBox.Show("Login Successful");
                this.Hide();
                var servicesScreen = new ServicesScreen(user);
                servicesScreen.Show();
            }
            else
            {
                MessageBox.Show("Login Failed");
                usrNameBox.Text = "";
                passwordBox.Password = "";
            }



        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usrNameBox.Text.Trim();
            string password = passwordBox.Password.Trim();


            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            string status = server.Register(username, password);

            if (status.Equals("succcessfully registered"))
            {
                MessageBox.Show("Registration Successful, you will be automatically logged in!");

                int token = server.Login(username, password);
                if (token != 0)
                {
                    user = new UserModel(username, token);
                    MessageBox.Show("Login Successful");
                    this.Hide();
                    var servicesScreen = new ServicesScreen(user);
                    servicesScreen.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    usrNameBox.Text = "";
                    passwordBox.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Registration Failed");
                usrNameBox.Text = "";
                passwordBox.Password = "";
            }


        }
    }
}
