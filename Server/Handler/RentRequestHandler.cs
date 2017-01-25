using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Server.Handler
{
    class RentRequestHandler : IHandleMessages<RentRequest>
    {
        static ILog log = LogManager.GetLogger<GetEquipmentListHandler>();

        public Task Handle(RentRequest message, IMessageHandlerContext context)
        {
            log.Info("RentRequest handling");

            try
            {
                var equipment = Data.Equipments[message.ItemId];
                Customer.Instance.AddToCart(equipment, message.DaysToRent);
            }
            catch // TODO: what to do here?
            {
            }
            RentResponse response = new RentResponse();
            return context.Reply(response);
        }
    }
}
