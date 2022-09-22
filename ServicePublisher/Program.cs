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
            int option = 0;

            Console.WriteLine("Welcome to the Service Publisher");

            int choice = mainText();
            switch (choice){
                case 1:
                    usr.token = Login();
                    option = publishText();
                    break;
                case 2:
                    //register using username and password
                    usr.token = Register();
                    option = publishText();

                    break;
                   
                case 3:
                    break;
                
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }


             int mainText(){
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Please enter your choice: ");
                
                int selection = Convert.ToInt32(Console.ReadLine());
                return selection;

            }


            int publishText(){
                Console.WriteLine("Please enter a command");
                Console.WriteLine("1. Publish");
                Console.WriteLine("2. Unpublish");
                Console.WriteLine("3. Exit");

                int command = Convert.ToInt32(Console.ReadLine());

                return command;

            }
            
            int Login()
            {
                //login using username and password
                Console.WriteLine("Enter username: ");
                usr.username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                usr.password = Console.ReadLine();

                AuthenticatorInterface server = initServer();
                int token = server.Login(usr.username, usr.password);
                if (token > 0)
                {
                    Console.WriteLine("User " + usr.username + "Has Logged in");
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

                AuthenticatorInterface server = initServer();
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

            

            switch (option)
            {
                case 1:
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
                    
                    string json = JsonConvert.SerializeObject(service1);
                   
                    string url = @"http://localhost:11252/api/publish?token=" + usr.token;
                    

                    var client = new RestClient(url);
                    var request = new RestRequest();
                    
                    request.AddJsonBody(json);
                    var response = client.Post(request);
                    Console.WriteLine("Status: " + response.Content.ToString());
                    option = publishText();



                    break;
                case 2:

                    Console.WriteLine("Enter the service name:");
                    string serviceName2 = Console.ReadLine();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

          

        }
        public static AuthenticatorInterface initServer()
        {
            AuthenticatorInterface server;
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost/AuthenticationService";
            var channelFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            server = channelFactory.CreateChannel();
            return server;
        }
    }
}
