using System;
using System.ComponentModel;

namespace P_6_Pricing_API.Models.Requests
{
    public class UserInputRequest
    {
        public int UserInputRequestId { get; set; }
        public int UserId { get; set; }
        [DefaultValue(1000)]
        public decimal Balance { get; set; }
        [DefaultValue("Variable")]
        public string? InterestType { get; set; }
        [DefaultValue("Loan")]
        public string? ProductType { get; set; }
        [DefaultValue("Principal only")]
        public string? PaymentType { get; set; }
        [DefaultValue(9)]
        public int OriginalMonth { get; set; }
        [DefaultValue(50)]
        public decimal CommitmentAmount { get; set; }
        [DefaultValue(2)]
        public decimal MonthlyFeeIncome { get; set; }
        [DefaultValue(0.03)]
        public decimal InterestSpread { get; set; }
        [DefaultValue(3)]
        public int TeaserPeriod { get; set; }
        [DefaultValue(0.04)]
        public decimal TeaserSpread { get; set; }
        [DefaultValue(0.08)]
        public decimal InterestRate { get; set; }
        [DefaultValue(5)]
        public int AvgMonthlyFeeIncome { get; set; }
        [DefaultValue(0.03)]
        public decimal DiaxountFromStandardFee { get; set; }
    }
}

