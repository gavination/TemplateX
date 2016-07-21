using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MVVMCrossTemplate.Services
{
    public class B2cAuthorizationService : IAuthService
    {
        public static string ClientID = "a6acc7da-decc-43ec-b15e-07d061d39d11";
        public static string[] Scopes = { ClientID };


        public static string SignUpSignInpolicy = "B2C_1_fb-signin";
        //public static string redirectURI = "urn:ietf:wg:oauth:2.0:oob";
        public static string Authority = "https://login.microsoftonline.com/jssample.onmicrosoft.com";
        private PublicClientApplication publicClientApp;


        public B2cAuthorizationService()
        {
            this.publicClientApp = new PublicClientApplication(Authority, ClientID);
        }

        public async Task<string> GetToken()
        {
            AuthenticationResult ar = await publicClientApp.AcquireTokenAsync(Scopes, "", UiOptions.SelectAccount, string.Empty, null, Authority, SignUpSignInpolicy);

            return ar.Token;
        }

        public PublicClientApplication client { get { return this.publicClientApp; } }
    }


    public interface IAuthService
    {
        Task<string> GetToken();
    }
}
