using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ServiceProvider.Services
{
    //connection to the Remoting Server is made to validate the token
    public class ValidateReq
    {
        private AuthenticatorInterface authenticatorServer;
        ChannelFactory<AuthenticatorInterface> authenticatorFactory;
        NetTcpBinding tcp = new NetTcpBinding();

        static string URL = "net.tcp://localhost/AuthenticationService";

        public ValidateReq()
        {
            authenticatorFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            authenticatorServer = authenticatorFactory.CreateChannel();
        }

        public string Validate(int token)
        {
            //Validate function in Remoting server is called.
            string validate = authenticatorServer.Validate(token);
            return validate;
        }

    }
}