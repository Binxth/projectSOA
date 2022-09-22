using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Authenticator Server starting...");
           
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(AuthenticatorImplementation));
            host.AddServiceEndpoint(typeof(AuthenticatorInterface), tcp, "net.tcp://localhost/AuthenticationService");
            host.Open();
         
            Console.WriteLine("Authenticator Online");


            
        
            //clear tokens in a new thread
            Thread clearTokens = new Thread(new ThreadStart(AuthenticatorImplementation.ClearTokens));
            clearTokens.IsBackground = true;
            clearTokens.Start();
            
            Console.ReadLine();
            host.Close();
        }



    }
}
