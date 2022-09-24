using Newtonsoft.Json;
using RestSharp;
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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for ServicesScreen.xaml
    /// </summary>
    public partial class ServicesScreen : Window
    {
        UserModel user;
        
      
        
        public ServicesScreen(UserModel user)
        {
            InitializeComponent();
            this.user = user;
            welcomeText.Text = "Hello " + user.username + " !";
        }

        private void getServicesButton_Click(object sender, RoutedEventArgs e)
        {
            

            RestClient restClient = new RestClient("http://localhost:11252/");

            RestRequest restRequest = new RestRequest("api/allservices", Method.Get);

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

        private void searchServiceButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient("http://localhost:11252/");

            RestRequest restRequest = new RestRequest("api/searchregistry", Method.Get);

            

            string search = searchBoxText.Text;

            restRequest.AddQueryParameter("description", search);
            restRequest.AddQueryParameter("token", user.token);


            RestResponse response = restClient.Execute(restRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
              
                if (response.Content == "[]")
                {
                    List<ServiceModel> allServices = new List<ServiceModel>();
                    allServices = JsonConvert.DeserializeObject<List<ServiceModel>>(response.Content);
                    allServiceTable.ItemsSource = allServices;
                }
                else
                {
                    MessageBox.Show("No services found");
                }
               
            }
            else
            {
                MessageBox.Show("Error: " + response.StatusCode);
            }



        }
        private void allServiceTable_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        { }
        }
}
