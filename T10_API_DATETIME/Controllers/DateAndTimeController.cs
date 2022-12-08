using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using T10_API_DATETIME.Models;

namespace T10_API_DATETIME.Controllers;

[ApiController]
[Route("[controller]")]
public class DateAndTimeController : ControllerBase
{
    private readonly Date_Time _dt;

    public DateAndTimeController()
    {
        _dt = new Date_Time();
    }

    [HttpGet("currentDate")]
    public string Current()
    {
        return _dt.CurrentDate.ToString("yyyy-MM-dd HH:mm");
    }

    [HttpGet("LondonDate")]
    public string CurrentLondonDate()
    {
        return _dt.CurrentLondonDate.ToString("yyyy-MM-dd HH:mm");
    }

    [HttpGet("FirstDayOfPreviousMonth")]
    public string FirstDayOfPreviousMonth()
    {
        return _dt.FirstDayOfPreviousMonth;
    }

    [HttpGet("LastDayOfPreviousMonth")]
    public string LastDayOfPreviousMonth()
    {
        return _dt.LastDayOfPreviousMonth;
    }

    [HttpGet("Difference")]
    public string DaysBetween(DateTime date1, DateTime date2)
    {
        return _dt.DaysBetween(date1, date2);
    }

    [HttpGet("DayInGeorgian")]
    public string DayInGeorgian()
    {
        return _dt.DayInGeorgian();
    }
}
