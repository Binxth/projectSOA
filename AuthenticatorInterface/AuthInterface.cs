using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticatorInterface
{
    [ServiceContract]
    public interface AuthInterface
    {
        [OperationContract]
        String Register(String username, String password);

        [OperationContract]
        int Login(String name, String password);

        [OperationContract]
        String Validate(int token);
    }
}
