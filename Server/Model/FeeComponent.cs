using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public interface FeeComponent
    {
        decimal ApplyFee(ref int daysLeft, CustomerRate rate);
    }

    public class OneTimeFee : FeeComponent
    {
        public decimal ApplyFee(ref int daysLeft, CustomerRate rate)
        {
            int amount = daysLeft > 0 ? 1 : 0;
            return rate.OneTimeFee * amount;
        }
    }

    public abstract class DailyFee : FeeComponent
    {
        protected int DaysLimit;

        public DailyFee(int days = int.MaxValue)
        {
            DaysLimit = days;
        }

        public abstract decimal OneDayFee(CustomerRate rate);

        public decimal ApplyFee(ref int daysLeft, CustomerRate rate)
        {
            int daysAmount = Math.Max(0, Math.Min(daysLeft, DaysLimit));
            daysLeft -= daysAmount;
            return daysAmount * OneDayFee(rate);
        }
    }

    public class PremiumFee : DailyFee
    {
        public PremiumFee(int days = int.MaxValue) 
            : base(days)
        {
        }

        public override decimal OneDayFee(CustomerRate rate)
        {
            return rate.PremiumDailyFee;
        }
    }


    public class RegularFee : DailyFee
    {
        public RegularFee(int days = int.MaxValue)
            : base(days)
        {
        }

        public override decimal OneDayFee(CustomerRate rate)
        {
            return rate.RegularDailyFee;
        }
    }
}
