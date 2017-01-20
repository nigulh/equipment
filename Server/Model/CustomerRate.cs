using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    // The fees are given in the currency
    public interface CustomerRate
    {
        Currency Currency { get; }
        decimal OneTimeFee();
        decimal PremiumDailyFee();
        decimal RegularDailyFee();
    }
}
