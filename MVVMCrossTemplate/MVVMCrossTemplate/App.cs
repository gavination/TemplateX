using Akavache;
using Microsoft.Identity.Client;
using MvvmCross.Platform.IoC;

namespace MVVMCrossTemplate
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {

        public static string ClientID = "";
        public static string[] Scopes = { ClientID };


        public static string SignUpSignInpolicy = "";
        public static string Authority = "https://login.microsoftonline.com/<yourserviceurl>/";
        public static PublicClientApplication PCApplication;

  
        public override void Initialize()
        {
#if DEBUG
            //clear cache while debugging
            BlobCache.LocalMachine.InvalidateAll();
#endif
            PCApplication = new PublicClientApplication(Authority, ClientID);

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.HomeViewModel>();
        }
    }
}
