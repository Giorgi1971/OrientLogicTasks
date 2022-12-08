using System;

namespace T10_API_DATETIME.Models
{
    public class Date_Time
    {
        static DateTime _current = DateTime.Now;

        public DateTime CurrentDate { get { return _current; } }
        public DateTime CurrentLondonDate { get { return _current.AddHours(-4); } }
        public string FirstDayOfPreviousMonth { get
            {
                var yr = DateTime.Today.Year;
                var mn = DateTime.Today.Month;
                var firstDay = new DateTime(yr, mn, 1);
                return firstDay.AddMonths(-1).ToString("yyyy-MM-dd");
            }
        }
        public string LastDayOfPreviousMonth { get
            {
                var yr = DateTime.Today.Year;
                var mn = DateTime.Today.Month;
                var firstDay = new DateTime(yr, mn, 1);
                return firstDay.AddDays(-1).ToString("yyyy-MM-dd");
            }
        }

        public bool IsLeapYear(DateTime date1)
            {
                var year = date1.Year;
                return DateTime.IsLeapYear(year);
            }

        public string DaysBetween(DateTime date1, DateTime date2)
        {
            TimeSpan diff = date1 - date2;
            var results = Math.Abs(diff.TotalDays);
            return $"{results}";
        }

        public string DayInGeorgian()
        {
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            Console.WriteLine((byte)wk);
            var Geodays = new Dictionary<byte, string>()
            {
                {1, "ორშაბათი"},{2, "სამშაბათი"},
                {3, "ოთხშაბათი"},{4, "სამშაბათი"},
                {5, "პარასკევი"},{6, "შაბათი"},{7, "კვირა"}
            };
            return Geodays[(byte)wk];
        }
    }
}

