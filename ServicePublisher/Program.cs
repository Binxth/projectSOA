using AuthInterface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserModel usr = new UserModel();
            int serviceOption = 0;


            //connection to Remoting Server

            AuthenticatorInterface server;
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost/AuthenticationService";
            var channelFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            server = channelFactory.CreateChannel();


            Console.WriteLine("///// Welcome to the Service Publisher /////");

            int initChoice = mainText();

            switch (initChoice)
            {
                case 1:
                    usr.token = Login();
                    break;
                case 2:
                    usr.token = Register();
                    break;

                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }



            while (serviceOption != 3)
            {
                serviceOption = serviceText();
                if (serviceOption == 1)
                {
                    publishService();
                }
                else if (serviceOption == 2)
                {
                    unpublishService();
                }
                else if (serviceOption == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
            }


            int mainText()
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Please enter your choice: ");

                int selection = Convert.ToInt32(Console.ReadLine());
                return selection;

            }


            int serviceText()
            {
                Console.WriteLine("Please enter a command");
                Console.WriteLine("1. Publish");
                Console.WriteLine("2. Unpublish");
                Console.WriteLine("3. Exit");
                Console.WriteLine("");

                if (int.TryParse(Console.ReadLine(), out int selection))
                {
                    return selection;
                }
                else
                {
                    return 0;
                }



            }

            int Login()
            {
                //login using username and password
                Console.WriteLine("Enter username: ");
                usr.username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                usr.password = Console.ReadLine();


                int token = server.Login(usr.username, usr.password);
                if (token > 0)
                {
                    Console.WriteLine( usr.username + " logged in");
                    return token;
                }
                else
                {

                    return 0;
                }
            }

            int Register()
            {
                Console.WriteLine("Enter username");
                usr.username = Console.ReadLine();
                Console.WriteLine("Enter password");
                usr.password = Console.ReadLine();


                string result = server.Register(usr.username, usr.password);

                if (result.Equals("successfully registered"))
                {
                    Console.WriteLine("User Registered");
                    int token = server.Login(usr.username, usr.password);
                    return token;


                }
                else
                {
                    Console.WriteLine("Failed");
                    return -1;
                }
            }

            //Publish service API request
            void publishService()
            {
                Console.WriteLine("Enter the service name:");
                string serviceName1 = Console.ReadLine();
                Console.WriteLine("Enter the service description:");
                string serviceDescription1 = Console.ReadLine();
                Console.WriteLine("Enter the service API endpoint:");
                string serviceAPIendpoint1 = Console.ReadLine();
                Console.WriteLine("Enter the number of operands:");
                int serviceNumOperands1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the operand type:");
                string serviceOperandType1 = Console.ReadLine();

                ServiceModel service1 = new ServiceModel(serviceName1, serviceDescription1, serviceAPIendpoint1, serviceNumOperands1, serviceOperandType1);
                Console.WriteLine("Service published");
                Console.WriteLine("Service name: " + service1.Name);
                Console.WriteLine("Service description: " + service1.Description);
                Console.WriteLine("Service API endpoint: " + service1.APIendpoint);
                Console.WriteLine("Service number of operands: " + service1.NumOperands);
                Console.WriteLine("Service operand type: " + service1.OperandType);


                RestClient restClient = new RestClient("http://localhost:11252/");
                RestRequest restRequest = new RestRequest("api/publish", Method.Post);

                string json = JsonConvert.SerializeObject(service1);
                restRequest.AddJsonBody(json);
                restRequest.AddQueryParameter("token", usr.token);
                RestResponse restResponse = restClient.Execute(restRequest);

                Console.WriteLine("Status: " + restResponse.Content.ToString());
                serviceOption = serviceText();

            }

            
            //unpublish service API request
            void unpublishService()
            {
                Console.WriteLine("Enter the service name:");
                string serviceName = Console.ReadLine();

                RestClient restClient = new RestClient("http://localhost:11252/");
                RestRequest restRequest = new RestRequest("api/unpublish", Method.Post);


                restRequest.AddQueryParameter("name", serviceName);
                restRequest.AddQueryParameter("token", usr.token);

                RestResponse restResponse = restClient.Execute(restRequest);
                Console.WriteLine("Status: " + restResponse.Content.ToString());
                serviceOption = serviceText();
            }
        }

    }
}
