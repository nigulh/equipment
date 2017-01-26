using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Client.Service
{
    public interface IServerMessageService
    {
        Task<T> GetResponse<T>(IMessage command) where T : IMessage;
    }

    public class ServerMessageService : IServerMessageService
    {
        IEndpointInstance endpoint;

        public ServerMessageService(IEndpointInstance endpoint) 
        {
            this.endpoint = endpoint;
        }

        public async Task<T> GetResponse<T>(IMessage command) where T : IMessage
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination(Configuration.GetServerAddress());
            var response = await endpoint.Request<T>(command, sendOptions).ConfigureAwait(false);
            return response;
        }

    }
}