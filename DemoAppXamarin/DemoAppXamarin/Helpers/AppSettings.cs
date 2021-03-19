using DemoAppXamarin.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.ObjectModel;

namespace DemoAppXamarin.Helpers
{
    public class AppSettings
    {
        private const string DefaultUserObjectId = "";

        public static ISettings Settings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current;

                return null;
            }
        }

        public static bool IsDataDownloaded
        {
            get
            {
                return Settings.GetValueOrDefault(nameof(IsDataDownloaded), false);
            }
            set
            {
                Settings.AddOrUpdateValue(nameof(IsDataDownloaded), value);
            }
        }
    }
}
