using System;
namespace CredoProject.Core.Models.Responses.ReportsResponce
{
    public class TransFeesAverageResponse
    {
        public string? TransactionType { get; set; }
        public List<DataByTransTypeAverage>? DataByTransTypeAverage { get; set; }

    }
}

