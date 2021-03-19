using DemoAppXamarin.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoAppXamarin.PageModels
{
    public class LandingPageModel : BasePageModel
    {
        public ICommand GetUserDetails { get; set; }

        public LandingPageModel()
        {
            GetUserDetails = new Command(OnUserDetailsClicked); 
        }

        private void OnUserDetailsClicked(object obj)
        {
            StaticHelper.InitializeAndShowMasterDetailPage();
        }
    }
}
