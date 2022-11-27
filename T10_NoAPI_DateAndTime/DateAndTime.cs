using System;
namespace T10_DateAndTime
{
    public static class DateAndTime
    {
        static DateTime dtn = DateTime.Now;

        public static void CurrentDate()
        {
            Console.Write("\nCurrent date is: ");
            Console.WriteLine(dtn.ToString("yyyy-MM-dd HH:mm")+"\n");
        }

        public static void CurrentLondonDate()
        {
            Console.Write("\nCurrent London datetime is: ");
            Console.WriteLine((dtn.AddHours(-4)).ToString("yyyy-MM-dd HH:mm") + "\n");
        }

        public static void DaysBetween()
        {
            var date1 = getDate("Enter first Date in Foramt yyyy-mm-dd");
            Console.WriteLine(date1);
            var date2 = getDate("Enter second Date in Foramt yyyy-mm-dd");
            TimeSpan diff = date1 - date2;
            Console.WriteLine($"\n{Math.Abs(diff.TotalDays)} days Difference in those two dates.\n");
        }

        public static void IsLeapYear()
        {
            Console.WriteLine("Enter Year in Foramt yyyy");
            var year = int.Parse(Console.ReadLine()!);
            Console.WriteLine(DateTime.IsLeapYear(year));
        }

        public static void FirstDayOfPreviousMonth()
        {
            var yr = DateTime.Today.Year;
            var mn = DateTime.Today.Month;
            var firstDay = new DateTime(yr, mn, 1);
            Console.WriteLine(firstDay.AddMonths(-1).ToString("yyyy-MM-dd"));
        }

        public static void LastDayOfPreviousMonth()
        {
            var yr = DateTime.Today.Year;
            var mn = DateTime.Today.Month;
            var firstDay = new DateTime(yr, mn, 1);
            Console.WriteLine(firstDay.AddDays(-1).ToString("yyyy-MM-dd"));
        }

        public static void CurrentWeekDay()
        {
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            Console.WriteLine((byte) wk);
            var Geodays = new Dictionary<byte, string>()
            {
                {1, "ორშაბათი"},{2, "სამშაბათი"},
                {3, "ოთხშაბათი"},{4, "სამშაბათი"},
                {5, "პარასკევი"},{6, "შაბათი"},{7, "კვირა"}
            };
            Console.WriteLine("\n"+Geodays[(byte) wk]+"\n");
        }

        static DateTime getDate(string str)
        {
            Console.WriteLine("Enter Date in Foramt yyyy-mm-dd");
            return Convert.ToDateTime(Console.ReadLine());
        }
    }
}

