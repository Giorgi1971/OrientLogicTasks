using P_6_Pricing_API.Repository;
using P_6_Pricing_API.Service;
using P_6_Pricing_API.Data;
using P_6_Pricing_API.Data.Entity;
using P_6_Pricing_API.Models;
using P_6_Pricing_API.Models.Requests;
using Azure.Core;
using NUnit.Framework.Constraints;

namespace P_6_Pricing_Test;

public class Tests
{
    private UserInputRequest _request;
    private DbInput _dbInput;
    private CalculatedInputs _calc;

    [SetUp]
    public void Setup()
    {
        UserInputRequest request = new()
        {
            Balance = 1000,
            InterestType = "Variable",
            ProductType = "Loan",
            PaymentType = "Principal only",
            OriginalMonth = 9,
            AvgMonthlyFeeIncome = 5,
            CommitmentAmount = 50,
            DiaxountFromStandardFee = 0.03m,
            InterestRate = 0.08m,
            InterestSpread = 0.03m,
            MonthlyFeeIncome = 2,
            TeaserPeriod = 3,
            TeaserSpread = 0.04m
        };
        DbInput dbInput = new()
        {
            CapitalRiskRateWeight = 0.015m,
            CreditRiskAllocation = "Capital",
            MaintenanceRate = 0.02m,
            PrepaymentRate = 0.07m
        };

        _dbInput = dbInput;
        _request = request;

        _calc = CalculatedInput.GetCalculatedInputs(_request, _dbInput);
        //_calc = calculate;
    }

    [Test]
    public void Test1()
    {

        var calc = CalculatedInput.GetCalculatedInputs(_request, _dbInput);
        //calc.TransactionCostRate.Equals(5.155);
        //calc.UsedPayment.Equals(45.15);
        Assert.That(calc.CapitalAllcationRate, Is.EqualTo(0.035));
        Assert.That(calc.InterestRate, Is.EqualTo(0.07));
        Assert.That(calc.TransactionCostRate, Is.EqualTo(5.1546m).Within(0.01));
        Assert.That(calc.UsedPayment, Is.EqualTo(45.155m).Within(0.01));
    }

    [Test]
    public void Test2()
    {
        var balance = new CalculateRepository();
        decimal excpected = balance.CalculateBalance(_request, _dbInput, _calc);
        Assert.That(excpected, Is.EqualTo(5240m).Within(111));

        _request.Balance = 2000;
        decimal excpected2 = balance.CalculateBalance(_request, _dbInput, _calc);
        Assert.That(excpected2, Is.EqualTo(10465).Within(111));
    }
}
