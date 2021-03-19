using DemoAppXamarin.Helpers;
using DemoAppXamarin.PageModels;
using DemoAppXamarin.WebServices;
using FreshMvvm;
using MonkeyCache.FileStore;
using Xamarin.Forms;

namespace DemoAppXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();

            var landingPageModel = FreshMvvm.FreshPageModelResolver.ResolvePageModel<LandingPageModel>();
            var loginContainer = new FreshNavigationContainer(landingPageModel, "LandingPage");
            var myPitchListViewContainer = new FreshTabbedNavigationContainer("UserListPage");

            Barrel.ApplicationId = "CachingDataSample";


            MainPage = loginContainer;

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterServices()
        {
            FreshIOC.Container.Register<IWebService, WebService>();
        }
    }
}
