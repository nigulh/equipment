using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class Invoice
    {
        public Invoice(Server.Customer customer, List<RentItem> items)
        {
            this.Customer = customer;
            this.Items = new List<RentItem>(items);
        }

        public Customer Customer { get; set; }
        public List<RentItem> Items { get; set; }
        public MoneyValue Price
        {
            get
            {
                return Items.Select(x => x.CalculatePrice(Customer))
                    .Aggregate(new MoneyValue(0, Customer.Currency), (total, it) => total + it);
            }
        }

        public int LoyaltyPoints
        {
            get { return Items.Sum(x => x.LoyaltyPoints); }
        }
    }
}
