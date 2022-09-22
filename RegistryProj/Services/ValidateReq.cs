

using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace RegistryProj.Services
{
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
            string validate = authenticatorServer.Validate(token);
            return validate;
        }

    }
}