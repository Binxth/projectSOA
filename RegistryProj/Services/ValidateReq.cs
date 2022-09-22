
using AuthInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace RegistryProj.Services
{
    public class ValidateReq
    {
        static private AuthenticatorInterface authenticatorServer;
        static  ChannelFactory<AuthenticatorInterface> authenticatorFactory;
        static  NetTcpBinding tcp = new NetTcpBinding();

        static string URL = "net.tcp://localhost/AuthenticationService";
        
         static ValidateReq()
        {
            authenticatorFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            authenticatorServer = authenticatorFactory.CreateChannel();
        }

        public static string Validate(int token)
        {
            string validate = authenticatorServer.Validate(token);
            return validate;
        }

    }
}