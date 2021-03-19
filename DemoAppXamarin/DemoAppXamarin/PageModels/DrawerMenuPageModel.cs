using DemoAppXamarin.Helpers;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoAppXamarin.PageModels
{
    public class DrawerMenuPageModel : BasePageModel
    {
        private List<Models.MenuItem> _menuList;
        public List<Models.MenuItem> MenuList
        {
            get
            {
                return _menuList;
            }
            set
            {
                _menuList = value;
                RaisePropertyChanged();
            }
        }

        private string _userEmail;
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                _userEmail = value;
                RaisePropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private string _appVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                RaisePropertyChanged();
            }
        }



        public ICommand LogoutCommand { get; set; }
        public ICommand HamburgerMenuCommand { get; set; }

        private Models.MenuItem _selectedMenu;

        public Models.MenuItem SelectedMenu
        {
            get
            {
                return _selectedMenu;
            }
            set
            {
                _selectedMenu = value;
                if (value != null)
                    MenuSelectedCommand.Execute(value);
            }
        }


        public DrawerMenuPageModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            MenuList = new List<Models.MenuItem>();
            MenuList.Add(new Models.MenuItem { Id =0, MenuName = "Home"});
            MenuList.Add(new Models.MenuItem { Id =1, MenuName = "User List"});

            UserName = "Test User";
            UserEmail = "test@test.com";
            AppVersion = "App Version: 1.0";
        }


        public Command<string> MenuSelectedCommand
        {
            get
            {
                return new Command<string>((item) =>
                {
                    switch (item)
                    {
                        case "Home":
                            {
                                StaticHelper.MenuIsPresented = false;

                                break;
                            }

                        case "UserList":
                            {
                                break;
                            }


                    }
                });
            }
        }
    }
}
