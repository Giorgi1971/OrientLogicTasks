using System;
using P_6_Pricing_API.Data;
using P_6_Pricing_API.Models.Requests;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using P_6_Pricing_API.Data.Entity;
using P_6_Pricing_API.Models;
//using P_6_Pricing_API.Models;
using P_6_Pricing_API.Service;
using Azure.Core;

namespace P_6_Pricing_API.Repository
{
    public interface ICalculateRepository
    {
        Task<decimal> CalculateEndingBalance(UserInputRequest request);
    }

    public class CalculateRepository : ICalculateRepository
    {
        private readonly PricingDbContext _db;

        public CalculateRepository()
        {
        }

        public CalculateRepository(PricingDbContext dbContext1)
        {
            _db = dbContext1;
        }

        public async Task<decimal> CalculateEndingBalance(UserInputRequest request)
        {
            var dbInpusts = await _db.DbInputs.FirstOrDefaultAsync();
            if (dbInpusts == null) throw new Exception();
            CalculatedInputs calcInputs = CalculatedInput.GetCalculatedInputs(request, dbInpusts);
            var result = CalculateBalance(request, dbInpusts, calcInputs);

            return result;
        }

        public decimal CalculateBalance(UserInputRequest request, DbInput dbInpusts, CalculatedInputs calcInputs)
        {
            var s3 = request.Balance;
            decimal sumTotal = 0;
            for (int j3 = 2; j3 < 14; j3++)
            {
                var k3 = calcInputs.UsedPayment*j3+calcInputs.UsedPayment*dbInpusts.MaintenanceRate + (request.InterestType=="Variable"?request.MonthlyFeeIncome+request.CommitmentAmount:0);
                var l3 = (request.PaymentType=="Interest only"||request.PaymentType=="Principal interest")?k3*request.InterestSpread+k3* request.InterestSpread *calcInputs.InterestRate:request.CommitmentAmount+request.MonthlyFeeIncome+k3*request.InterestSpread;
                var m3 = (-(k3-l3)>s3)?-s3:Math.Min(0,k3-l3);
                var n3 = (j3>=request.OriginalMonth?-s3:0);
                var o3 = m3 + n3;
                var p3 = (-o3>=s3)?0:Math.Max(-(s3+o3),-dbInpusts.PrepaymentRate*s3);
                var s4 = o3 + p3 + (p3 * calcInputs.CapitalAllcationRate);
                var result = s3 + s4;
                Console.WriteLine(j3+" - "+result);
                sumTotal += result;
                s3 = result;
            }
            return sumTotal;
        }
    }
}
