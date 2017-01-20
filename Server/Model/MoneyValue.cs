using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class MoneyValue
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public MoneyValue(decimal amount, Currency cur) 
        {
            this.Amount = amount;
            this.Currency = cur;
        }

        public static MoneyValue operator +(MoneyValue c1, MoneyValue c2)
        {
            if (c1.Currency != c2.Currency) throw new ArgumentException("Currency mismatch");
            return new MoneyValue(c1.Amount + c2.Amount, c1.Currency);
        }

        public static MoneyValue operator -(MoneyValue c1, MoneyValue c2)
        {
            if (c1.Currency != c2.Currency) throw new ArgumentException("Currency mismatch");
            return new MoneyValue(c1.Amount - c2.Amount, c1.Currency);
        }

        public static MoneyValue operator *(MoneyValue c1, int m)
        {
            return new MoneyValue(c1.Amount * m, c1.Currency);
        }

        public static MoneyValue operator *(int m, MoneyValue c1)
        {
            return new MoneyValue(c1.Amount * m, c1.Currency);
        }

        public static bool operator ==(MoneyValue c1, MoneyValue c2)
        {
            if (object.ReferenceEquals(c1, null) != object.ReferenceEquals(c2, null))
            {
                return false;
            }
            if (object.ReferenceEquals(c1, null))
            {
                return false;
            }
            return c1.Currency == c2.Currency && c1.Amount == c2.Amount;
        }

        public static bool operator !=(MoneyValue c1, MoneyValue c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            var second = o as MoneyValue;

            return second != null && this == second;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(Amount % 1000000007);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Amount, Currency);
        }
    }
}
