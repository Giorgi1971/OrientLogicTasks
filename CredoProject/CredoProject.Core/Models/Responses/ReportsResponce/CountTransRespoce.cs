using System;
namespace CredoProject.Core.Models.Responses.ReportsResponce
{
    public class CountTransRespoce
    {
        public string? DatePeriod { get; set; }
        public List<DataByPeriodCount>? DataByPeriod { get; set; }
    }
}

