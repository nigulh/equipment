using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using NServiceBus;
using Shared;
using System.Threading.Tasks;

namespace AsyncPagesMVC.Service
{
    public class Util
    {
        public static async Task<T> GetServerResponse<T>(IEndpointInstance endpoint, IMessage command) where T : IMessage
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination(Configuration.GetServerAddress());
            var response = await endpoint.Request<T>(command, sendOptions).ConfigureAwait(false);
            return response;
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