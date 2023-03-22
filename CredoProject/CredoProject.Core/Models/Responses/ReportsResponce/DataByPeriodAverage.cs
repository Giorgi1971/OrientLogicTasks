using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Responses.ReportsResponce
{
    public class DataByPeriodAverage
    {
        public decimal? AverageOfFees { get; set; }
        public string? Currency { get; set; }
    }
}
