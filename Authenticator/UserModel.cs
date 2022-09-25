using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//UserModel is used to deserialize and serialize the user data which is passed and stored in JSON format.
namespace Authenticator
{
    public class UserModel
    {
        public UserModel()
        {
            username = "";
            password = "";
        }
        public UserModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
       
        public String username { get; set; }
        public String password { get; set; }
    }
}
