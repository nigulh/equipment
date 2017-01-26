using NServiceBus;
using NServiceBus.Logging;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handler
{
    class InvoiceRequestHandler : IHandleMessages<InvoiceRequest>
    {
        static ILog log = LogManager.GetLogger<InvoiceRequestHandler>();

        public Task Handle(InvoiceRequest message, IMessageHandlerContext context)
        {
            log.Info("InvoiceRequest handling");

            var invoice = Customer.Instance.PlaceOrder();
            if (invoice == null) // No invoice was generated
            {
                return context.Reply(new InvoiceResponse()); // return some Error?
            }
            var customer = invoice.Customer;
            var response = new InvoiceResponse()
            {
                Id = invoice.Id,
                Items = invoice.Items.Select((item) => ConvertItem(item, customer)).ToList()
            };

            return context.Reply(response);
        }

        static Dictionary<Server.Model.Currency, Shared.Messages.Currency> currencyMap = new Dictionary<Model.Currency, Currency>() {
            {Server.Model.Currency.EUR, Currency.EUR},
            {Server.Model.Currency.USD, Currency.USD}
        };

        private static InvoiceItem ConvertItem(Model.RentItem item, Customer customer)
        {
            var price = item.CalculatePrice(customer);
            return new InvoiceItem()
            {
                Currency = currencyMap[price.Currency],
                Price = price.Amount,
                DaysToRent = item.DaysToRent,
                LoyaltyPoints = item.LoyaltyPoints,
                Name = item.Item.Name
            };
        }

    }
}
