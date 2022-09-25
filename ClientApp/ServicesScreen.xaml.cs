using Newtonsoft.Json;
using RestSharp;
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
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for ServicesScreen.xaml
    /// </summary>
    public partial class ServicesScreen : Window
    {
        UserModel user;


        //User object with token and name is passed from the login screen
        //inorder to append the user token to each request
        public ServicesScreen(UserModel user)
        {
            InitializeComponent();
            this.user = user;
            welcomeText.Text = "Hello " + user.username + "!";
        }

        private void getServicesButton_Click(object sender, RoutedEventArgs e)
        {


            //creating the rest client with server URL
            RestClient restClient = new RestClient("http://localhost:11252/");

            //endpoint 
            RestRequest restRequest = new RestRequest("api/allservices", Method.Get);

            //adding the logged in users token to the URL as a Query Parameter
            restRequest.AddQueryParameter("token", user.token);
            RestResponse response = restClient.Execute(restRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ServiceModel> allServices = new List<ServiceModel>();
                allServices = JsonConvert.DeserializeObject<List<ServiceModel>>(response.Content);
                List<ServiceModel> gridData = new List<ServiceModel>();
                allServiceTable.ItemsSource = allServices;
            }
            else
            {
                MessageBox.Show("Error: " + response.StatusCode);
            }

        }

        //search request is sent asynchronously
        private async void searchServiceButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            bool isString = Regex.IsMatch(searchBoxText.Text, "^[a-zA-Z]+$");
            if (!isString)
            {
                MessageBox.Show("Invalid Characters in Search");
                return;
            }
            string search = searchBoxText.Text;
            
            RestClient restClient = new RestClient("http://localhost:11252/");
            RestRequest restRequest = new RestRequest("api/searchregistry", Method.Get);


            restRequest.AddQueryParameter("description", search);
            restRequest.AddQueryParameter("token", user.token);

            //make async request
            Task<RestResponse> asyncResponse =  restClient.ExecuteAsync(restRequest);
            //progress bar is activated until the async request is completed
            progressBar.IsIndeterminate = true;
            RestResponse response = await asyncResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                if (response.Content != "[]")
                {
                    List<ServiceModel> allServices = new List<ServiceModel>();
                    allServices = JsonConvert.DeserializeObject<List<ServiceModel>>(response.Content);
                    allServiceTable.ItemsSource = allServices;
                    progressBar.IsIndeterminate = false;
                }
                else
                {
                    MessageBox.Show("No services found");
                    progressBar.IsIndeterminate = false;
                }

            }
            else
            {
                MessageBox.Show("Error: " + response.StatusCode);
            }



        }
        private void allServiceTable_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ServiceModel selected = allServiceTable.SelectedItem as ServiceModel;
            if (selected.Name == null)
            {
                MessageBox.Show("empty");
            }
            else
            { 
                labelDesc.Content = selected.Description;
                labelAPI.Content = selected.APIendpoint;
            }

            if (selected.NumOperands == 2)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Visible;
                txtbox3.Visibility = Visibility.Hidden;
                CalcButton.Visibility = Visibility.Visible;
            }
            else if (selected.NumOperands == 3)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Visible;
                txtbox3.Visibility = Visibility.Visible;
                CalcButton.Visibility = Visibility.Visible;
            }
            else if (selected.NumOperands == 1)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Hidden;
                txtbox3.Visibility = Visibility.Hidden;
                CalcButton.Visibility = Visibility.Visible;
            }

        }
        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient;
            ServiceModel selected = allServiceTable.SelectedItem as ServiceModel;

            restClient = new RestClient(selected.APIendpoint.ToString());

            var restRequest = new RestRequest();

            restRequest.Method = Method.Get;

            if (selected.NumOperands == 1)
            {
                restRequest.AddQueryParameter("num1", txtbox1.Text);
            }
            else if (selected.NumOperands == 2)
            {
                restRequest.AddQueryParameter("num1", txtbox1.Text);
                restRequest.AddQueryParameter("num2", txtbox2.Text);
            }
            else if (selected.NumOperands == 3)
            {
                restRequest.AddQueryParameter("num1", txtbox1.Text);
                restRequest.AddQueryParameter("num2", txtbox2.Text);
                restRequest.AddQueryParameter("num3", txtbox3.Text);
            }

            restRequest.AddQueryParameter("token", user.token);

            RestResponse response = restClient.Execute(restRequest);
            CalcResponseModel calcSum = new CalcResponseModel();
            calcSum = JsonConvert.DeserializeObject<CalcResponseModel>(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(calcSum.sum.ToString());
            }
            else
            {
                MessageBox.Show("Error: " + response.StatusCode);
            }

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            var mainWindow = new MainWindow();
            mainWindow.Show();

        }
    }
}
