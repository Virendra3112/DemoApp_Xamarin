using DemoAppXamarin.Helpers;
using DemoAppXamarin.Models;
using DemoAppXamarin.WebServices;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAppXamarin.PageModels
{
    public class UserListPageModel : BasePageModel
    {

        #region Properties
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> PersonList
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                RaisePropertyChanged();
            }
        }


        private Person _selectedItem = null;
        public Person SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (value != null)
                {
                    CoreMethods.DisplayAlert("User Details", "Id: " + _selectedItem.Id +
                        " Name: " + _selectedItem.FirstName
                        , "Ok");
                }

            }
        }

        private readonly IWebService _webService;

        #endregion

        #region Constructor
        public UserListPageModel(IWebService webService)
        {
            _webService = webService;
        }
        #endregion

        #region Overrided Methods
        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (AppSettings.IsDataDownloaded)
            {
                await GetOfflineDataAsync();
            }
            else
            {
                await GetUserDataAsync();
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
        #endregion

        #region Public/Private Methods
        private async Task GetUserDataAsync()
        {
            try
            {
                IsBusy = true;

                if (IsConnected)
                {
                    var _apiResult = await _webService.GetData<UserData>(APIEndPoints.NamesUri);

                    if (_apiResult != null)
                    {
                        var userData = JsonConvert.DeserializeObject<UserData>(_apiResult);

                        if (userData != null && userData.People.Any())
                        {
                            PersonList = new ObservableCollection<Person>(userData.People);

                            SaveDataToLocalCache();
                        }
                    }
                }
                else
                {
                    await CoreMethods.DisplayAlert("Error", "No Internet", "Ok");
                }
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", "Something went wrong.", "Ok");
            }

            finally { IsBusy = false; }
        }


        private async Task GetOfflineDataAsync()
        {
            try
            {
                var _persons = Barrel.Current.Get<IEnumerable<Person>>(key: "Persons");

                PersonList = new ObservableCollection<Person>(_persons);

            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", "Something went wrong.", "Ok");
            }

        }

        private void SaveDataToLocalCache()
        {
            try
            {
                Barrel.Current.Add(key: "Persons", data: PersonList, expireIn: TimeSpan.FromDays(100));
                AppSettings.IsDataDownloaded = true;
            }
            catch (Exception ex)
            {
                CoreMethods.DisplayAlert("Error", "Something went wrong.", "Ok");
            }

        }
        #endregion

    }
}
