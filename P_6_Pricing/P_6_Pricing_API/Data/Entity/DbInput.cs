using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P_6_Pricing_API.Data.Entity
{
    public class DbInput
    {
        public int DbInputId { get; set; }
        public decimal MaintenanceRate { get; set; }
        public decimal PrepaymentRate { get; set; }
        public string? CreditRiskAllocation { get; set; }
        public decimal CapitalRiskRateWeight { get; set; }
    }
}
