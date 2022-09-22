using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublisher
{
    internal class UserModel
    {
        
        public string username { get; set; }
        public string password { get; set; }
        public int token { get; set; }

        public UserModel()
        {
        }
        public UserModel(string username, int token)
        {
            username = username;
            
            
            token = token;
        }


    }
}
