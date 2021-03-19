using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;

namespace DemoAppXamarin.PageModels
{
    public class BasePageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        #region Properties
        public bool IsConnected
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        bool isBusy;

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                ShowLoading(value);
                RaisePropertyChanged();
            }
        }

        private bool _isNetworkAvailable;

        public bool IsNetworkAvailable
        {
            get => _isNetworkAvailable;
            set
            {
                _isNetworkAvailable = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public BasePageModel()
        {
            CheckConnectivity();
        }

        #endregion

        #region Methods
        private void ShowLoading(bool value)
        {
            try
            {

                if (value)
                    UserDialogs.Instance.ShowLoading("Loading...", MaskType.Black);

                else
                    UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckConnectivity()
        {
            try
            {
                CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
                {
                    IsNetworkAvailable = args.IsConnected;

                    ToastConfig.DefaultPosition = ToastPosition.Top;

                    if (IsNetworkAvailable)
                    {
                        UserDialogs.Instance.Toast("Internet connected");

                    }

                    else
                        UserDialogs.Instance.Toast("Internet Lost");

                };
            }
            catch (System.Exception ex)
            {

            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected new void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #endregion

    }
}
