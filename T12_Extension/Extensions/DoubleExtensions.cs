using System;

namespace ExtensionMethods
{
    public static class DoubleExtensions
    {
        public static double ToPercent(this double d)
        {   
            return 100 * d;
        }

        public static Int32 RoundDown(this double i)
        {
            string str = Convert.ToString(i);
            return (Int32)Convert.ToInt32(str.Split(",")[0]);
        }

        public static decimal ToDecimal(this double num1)
        {
            decimal num = (decimal)Convert.ToDecimal(num1);
            return num;
        }

        public static bool IsGreater(this double d, double value)
        {
            return d > value;
        }

        public static bool IsLess(this double d, double value)
        {
            return d < value;
        }
    }
}

