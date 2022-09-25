using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Interface of .NET remoting server (Aithenticator) is taken into a seperate project for the purpose of reusability.
namespace AuthInterface
{
    [ServiceContract]
    public interface AuthenticatorInterface
    {
        [OperationContract]
        String Register(String username, String password);

        [OperationContract]
        int Login(String name, String password);

        [OperationContract]
        String Validate(int token);
    }
}
