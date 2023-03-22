using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Responses.ReportsResponce
{
    public class DataByPeriodCount
    {
        public string? TransactionsType { get; set; }
        public int TransactionsQuantity { get; set; }
    }
}
