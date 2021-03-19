﻿using DemoAppXamarin.Helpers;
using DemoAppXamarin.Models;
using DemoAppXamarin.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
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

            await GetUserDataAsync();
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

                    var userData = JsonConvert.DeserializeObject<UserData>(_apiResult);

                    PersonList = new ObservableCollection<Person>(userData.People);

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
        #endregion

    }
}
