using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class InvoiceRequest : IMessage
    {
    }

    public class InvoiceResponse : IMessage
    {
        public int Id;
        public List<InvoiceItem> Items = new List<InvoiceItem>();
    }

    public class InvoiceItem
    {
        public string Name { get; set; }
        public int DaysToRent { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public int LoyaltyPoints { get; set; }
    }

    public enum Currency
    {
        EUR, USD
    }
}
