using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public abstract class Equipment
    {
        public string Name { get; set; }
        public string Url { get; set; }

        protected abstract IEnumerable<FeeComponent> RentalFeeComponents();

        public virtual int RentalLoyalityPoints() {
            return 1;
        }

        public MoneyValue CalculateRentalPrice(int days, CustomerRate rate)
        {
            var price = 0m;
            foreach (var component in RentalFeeComponents())
            {
                price += component.ApplyFee(ref days, rate);
            }
            return new MoneyValue(price, rate.Currency);
        }
    }

    public class HeavyEquipment : Equipment
    {
        static List<FeeComponent> FeeComponents = new List<FeeComponent>() {
            new OneTimeFee(),
            new PremiumFee()
        };

        protected override IEnumerable<FeeComponent> RentalFeeComponents()
        {
            return HeavyEquipment.FeeComponents;
        }

        public override int RentalLoyalityPoints()
        {
            return 2;
        }
    }

    public class RegularEquipment : Equipment
    {
        static List<FeeComponent> FeeComponents = new List<FeeComponent>() {
            new OneTimeFee(),
            new PremiumFee(2),
            new RegularFee()
        };

        protected override IEnumerable<FeeComponent> RentalFeeComponents()
        {
            return RegularEquipment.FeeComponents;
        }
    }

    public class SpecializedEquipment : Equipment
    {
        static List<FeeComponent> FeeComponents = new List<FeeComponent>() {
            new OneTimeFee(),
            new PremiumFee(3),
            new RegularFee()
        };

        protected override IEnumerable<FeeComponent> RentalFeeComponents()
        {
            return SpecializedEquipment.FeeComponents;
        }
    }
}
