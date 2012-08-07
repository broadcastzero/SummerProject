/*
 * This class is responsible for reading and writing isolated storage settings
 * Thanks to Jacob and cobbal
 * http://stackoverflow.com/questions/3145803/windows-phone-7-config-appsettings
 */

namespace SummerProjectWp7.Helpers
{
    using System;
    using System.IO.IsolatedStorage;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    public static class AppSettings
    {
        private static IsolatedStorageSettings Settings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;

        public static void StoreSetting(string settingName, string value)
        {
            StoreSetting<string>(settingName, value);
        }

        public static void StoreSetting<TValue>(string settingName, TValue value)
        {
            if (!Settings.Contains(settingName))
                Settings.Add(settingName, value);
            else
                Settings[settingName] = value;

            // EDIT: if you don't call Save then WP7 will corrupt your memory!
            Settings.Save();
        }

        public static bool TryGetSetting<TValue>(string settingName, out TValue value)
        {            
            if (Settings.Contains(settingName))
            {
                value = (TValue)Settings[settingName];
                return true;
            }

            value = default(TValue);
            return false;
        }
    }
}
