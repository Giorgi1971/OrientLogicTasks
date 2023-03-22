using System;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Responses.ReportsResponce
{
    public class SumFeesTransactionsResponse
    {
        public string? DatePeriod { get; set; }
        public List<DataByPeriod>? DataByPeriod { get; set; }
    }
}
