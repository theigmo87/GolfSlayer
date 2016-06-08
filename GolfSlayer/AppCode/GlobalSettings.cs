using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;
using Repositories.Entities;

namespace GolfSlayer
{
    public static class GlobalSettings
    {        
        private static bool _initialized;
        private static SettingsRepository _settings;
        public static void Init()
        {
            if (!_initialized)
            {
                _settings = new SettingsRepository();
                _initialized = true;
            }
        }

        public static String DonateURL
        {
            get
            {
                return GetSettingString("DonateURL");
            }
            set
            {
                UpdateSetting("DonateURL", value);
            }
        }

        public static String ChampionsURL
        {
            get
            {
                return GetSettingString("ChampionsURL");
            }
            set
            {
                UpdateSetting("ChampionsURL", value);
            }
        }

        public static String Rules
        {
            get
            {
                return GetSettingString("Rules");
            }
            set
            {
                UpdateSetting("Rules", value);
            }
        }

        public static String SafeHomesURL
        {
            get
            {
                return GetSettingString("SafeHomesURL");
            }
            set
            {
                UpdateSetting("SafeHomesURL", value);
            }
        }
        


        #region Helpers
        private static String GetSettingString(String key)
        {
            Settings setting = _settings.GetByID(key);
            if (setting == null)
            {
                setting = UpdateSetting(key, "");
            }
            return setting.Value;
        }
        private static Boolean GetSettingsBoolean(String key)
        {
            Settings setting = _settings.GetByID(key);
            if (setting == null)
            {
                setting = UpdateSetting(key, "false");
            }
            Boolean retVal = false;
            Boolean.TryParse(setting.Value, out retVal);
            return retVal;
        }
        private static int GetSettingsInteger(String key)
        {
            Settings setting = _settings.GetByID(key);
            if (setting == null)
            {
                setting = UpdateSetting(key, "0");
            }
            int retVal = 0;
            int.TryParse(setting.Value, out retVal);
            return retVal;
        }
        private static Settings UpdateSetting(String key, String value = "")
        {
            return _settings.InsertOrUpdate(new Settings() { Key = key, Value = value, DateUpdated = DateTime.Now, DateInserted = DateTime.Now });
        }
        #endregion
    }
}