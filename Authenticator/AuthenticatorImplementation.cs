using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]

    internal class AuthenticatorImplementation : AuthenticatorInterface
    {

        
        public String Register(String username, String password)
        {
            //relative file path is used to store the username and password
            //Eg: C:\Users\Binxth\Desktop\DC Assignment\projectSOA\Authenticator\bin\Debug\clients.txt
            using (StreamWriter writer = new StreamWriter("clients.txt", true))

            {
                UserModel user = new UserModel(username, password);

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                writer.WriteLine(json);
                writer.Close();
            }
            return "succcessfully registered";
        }


        public int Login(String username, String password)
        {
            string[] lines = File.ReadAllLines("clients.txt");
            foreach (string line in lines)
            {
                UserModel user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(line);
                if (user.username == username && user.password == password)
                {
                    Random rnd = new Random();
                    int token = rnd.Next(100000, 999999);
                    //relative file path is used to store the tokens
                    //Eg: C:\Users\Binxth\Desktop\DC Assignment\projectSOA\Authenticator\bin\Debug\tokens.txt

                    using (StreamWriter writer = new StreamWriter("tokens.txt", true))
                    {
                        writer.WriteLine(token);
                        writer.Close();
                    }
                    Console.WriteLine(username + " Logged in: " + token.ToString());
                    return token;
                }
            }
            return 0;
        }



        public String Validate(int token)
        {
            string[] lines = File.ReadAllLines("tokens.txt");
            foreach (string line in lines)
            {
                if (line == token.ToString())
                {
                    Console.WriteLine(token.ToString() + " Validated");
                    return "validated";
                }
            }
            return "not validated";
        }

        public static void ClearTokens()
        {
            while (true)
            {
                //clearing the tokens in evey 5 minutes
                System.Threading.Thread.Sleep(1000 * 60 * 5);
                File.WriteAllText("tokens.txt", string.Empty);
                Console.WriteLine("Tokens cleared!!");
                
                
            }
        }

    }
}

