using System;
using DemoAppXamarin.PageModels;
using FreshMvvm;
using Xamarin.Forms;

namespace DemoAppXamarin.Helpers
{
    public class StaticHelper
    {
        private static FreshMasterDetailNavigationContainer masterDetailNav;

        public static bool MenuIsPresented
        {
            get
            {
                return masterDetailNav.IsPresented;
            }
            set
            {
                masterDetailNav.IsPresented = value;
            }
        }

        public static void InitializeAndShowMasterDetailPage()
        {
            masterDetailNav = new FreshMasterDetailNavigationContainer();

            var drawerMenuPage = FreshPageModelResolver.ResolvePageModel<DrawerMenuPageModel>();
            var userDetails = FreshPageModelResolver.ResolvePageModel<UserListPageModel>();
            var basicNavigationContainer = new NavigationPage(userDetails)
            {
                //BarBackgroundColor = (Color)Application.Current.Resources["secondaryColor"],
                BarTextColor = (Color.White)
            };
            masterDetailNav.Master = drawerMenuPage;
            masterDetailNav.Detail = basicNavigationContainer;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    masterDetailNav.IsGestureEnabled = false;
                    break;

                case Device.Android:
                    masterDetailNav.IsGestureEnabled = true;
                    break;

                default:
                    break;
            }
            Application.Current.MainPage = masterDetailNav;
        }
    }
}