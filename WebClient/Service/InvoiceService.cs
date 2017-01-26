using NServiceBus;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Client.Service
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoice();
    }

    public class InvoiceService : IInvoiceService
    {
        IServerMessageService server;

        public InvoiceService(IServerMessageService server)
        {
            this.server = server;
        }

        public async Task<InvoiceResponse> GetInvoice()
        {
            var response = await server.GetResponse<InvoiceResponse>(new InvoiceRequest());

            return response;
        }
    }
}