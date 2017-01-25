using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class RentRequest : IMessage
    {
        public int ItemId { get; set; }
        public int DaysToRent { get; set; }
    }

    public class RentResponse : IMessage
    {

    }
}
