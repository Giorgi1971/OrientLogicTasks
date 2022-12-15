using System;

namespace ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime Min(this DateTime i, DateTime date)
        {
            if (i > date)
                return date;
            else
                return i;
        }

        public static DateTime Max(this DateTime i, DateTime date)
        {
            if (i < date)
                return date;
            else
                return i;
        }

        public static DateTime BeginingOfMonth(this DateTime i)
        {
            var date = new DateTime(i.Year, i.Month, 1);
            return date;
        }

        public static DateTime EndOfMonth(this DateTime i)
        {
            var date = new DateTime(i.Year, i.Month, 1);
            return date.AddMonths(1).AddDays(-1);
        }
    }
}
