using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Template.Web.Models
{
    public static class ConfigUtil
    {
        public static string GetConnectionString(string dbname)
        {
            var str = ConfigUtil.GetAppSetting("ConnectionStringTemplate", "");
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("Unable to find ConnectionStringTemplate in Web.config");
            }
            return string.Format(str, dbname);
        }

        public static string GetAppSetting(string name, string defaultValue)
        {
            var str = ConfigurationManager.AppSettings[name];
            if (str == null) str = defaultValue;
            return str;
        }

        public static int GetAppSetting(string name, int defaultValue)
        {
            var str = ConfigurationManager.AppSettings[name];
            int num;
            if (!int.TryParse(str, out num))
            {
                num = defaultValue;
            }
            return num;
        }

    }
}