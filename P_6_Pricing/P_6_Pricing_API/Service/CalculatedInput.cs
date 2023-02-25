using System;
using P_6_Pricing_API.Data;
using P_6_Pricing_API.Models;
using P_6_Pricing_API.Models.Requests;
using P_6_Pricing_API.Data.Entity;

namespace P_6_Pricing_API.Service
{
    public class CalculatedInput
    {
        private readonly PricingDbContext _db;

        public CalculatedInput(PricingDbContext db)
        {
            _db = db;
        }

        public static CalculatedInputs GetCalculatedInputs(UserInputRequest request, DbInput dbInputs)
        {
            var interestRate = ((request.ProductType == "Loan" || request.ProductType == "CD") && request.InterestType == "Fixed") ? request.InterestRate : (request.TeaserPeriod == 0 ? request.TeaserSpread : request.TeaserSpread + request.InterestSpread);
            var transactionCostRate = request.AvgMonthlyFeeIncome / (1 - request.DiaxountFromStandardFee);
            var capitalAllcationRate = (dbInputs.CreditRiskAllocation == "Capital") ? dbInputs.MaintenanceRate + dbInputs.CapitalRiskRateWeight : dbInputs.MaintenanceRate;
            var usedPayment = (request.InterestType == "Fixed")?request.Balance*request.InterestSpread:request.Balance*request.TeaserSpread;
            var calcIn = new CalculatedInputs
            {
                InterestRate = interestRate,
                TransactionCostRate = transactionCostRate,
                CapitalAllcationRate = capitalAllcationRate,
                UsedPayment = usedPayment + transactionCostRate
            };
            return calcIn;
        }
    }
}
