using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using NServiceBus;
using Shared;
using System.Threading.Tasks;
using System.Resources;

namespace Client.Service
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum e)
        {
            // TODO: Make it work
            var rm = new ResourceManager(typeof(Resources.Enums).Name, typeof(Resources.Enums).Assembly);
            var resourceDisplayName = rm.GetString(e.GetType().Name + "_" + e);

            return string.IsNullOrWhiteSpace(resourceDisplayName) ? string.Format("[[{0}]]", e) : resourceDisplayName;
        }
    }

    public static class Configuration
    {
        private static string _serverAddress = null;

        public static int getKeyValInt(string key)
        {
            int j;
            bool b = Int32.TryParse(ConfigurationManager.AppSettings[key], out j);
            if (b)
                return j;
            else
                return 0;
        }

        public static string getKeyVal(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetServerAddress()
        {
            if (_serverAddress == null)
            {
                _serverAddress = getKeyVal("ServerAddress") ?? "Samples.Mvc.Server";
            }
            return _serverAddress;
        }
    }
}