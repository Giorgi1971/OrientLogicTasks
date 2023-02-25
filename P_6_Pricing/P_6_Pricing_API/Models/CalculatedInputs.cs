using System;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P_6_Pricing_API.Models
{
    public class CalculatedInputs
    {
        public decimal InterestRate { get; set; }
        public decimal TransactionCostRate { get; set; }
        public decimal CapitalAllcationRate { get; set; }
        public decimal UsedPayment { get; set; }
    }
}

