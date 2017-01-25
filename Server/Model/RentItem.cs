using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class RentItem
    {
        public Equipment Item { get; private set; }
        private int _daysToRent;
        public int DaysToRent
        {
            get { return _daysToRent; }
            private set
            {
                if (value <= 0) throw new ArgumentException("DaysToRent must be positive");
                _daysToRent = value;
            }
        }

        public RentItem(Model.Equipment item, int daysToRent)
        {
            this.Item = item;
            this.DaysToRent = daysToRent;
        }

        public MoneyValue CalculatePrice(CustomerRate customer)
        {
            return this.Item.CalculateRentalPrice(this.DaysToRent, customer);
        }

        public int LoyaltyPoints { get { return Item.RentalLoyalityPoints; } }
    }




}
