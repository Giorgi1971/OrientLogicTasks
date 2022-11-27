using T10_DateAndTime;

start();

static void start()
{
    while(true)
        {
        printMenu();
        var switch_on = Console.ReadLine();
        switch (switch_on)
        {
            case "1": DateAndTime.CurrentDate(); break;
            case "2": DateAndTime.CurrentLondonDate(); break;
            case "3": DateAndTime.DaysBetween(); break;
            case "4": DateAndTime.IsLeapYear(); break;
            case "5": DateAndTime.FirstDayOfPreviousMonth(); break;
            case "6": DateAndTime.LastDayOfPreviousMonth(); break;
            case "7": DateAndTime.CurrentWeekDay(); break;
            case "9": Environment.Exit(0); break;
            default: Console.WriteLine("\nEnter Correct number!\n"); break;
        }
    }
}


static void printMenu()
{
    Console.WriteLine("1 CurrentDate    \t\t2 LondonCurrentDate");
    Console.WriteLine("3 DaysBetween    \t\t4 IsLeapYear");
    Console.WriteLine("5 FirstDayOfPreviousMonth \t6 LastDayOfPreviousMonth");
    Console.WriteLine("7 CurrentWeekDay \t\t9 Exit");
}
