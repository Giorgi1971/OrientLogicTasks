using CredoProject.Core.Calculates;
using CredoProject.Core.Models.Requests.Card;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Responses;
using CredoProject.Core.Models.Responses.ReportsResponce;
using CredoProject.Core.Validations;
using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.ComponentModel.DataAnnotations;


namespace CredoProject.Core.Services
{
    public interface IReportsService
    {
        Task<List<JsonUserStatisticResponse>> ViewRegistredCustomersStatistic(); //+
        Task<List<CountTransRespoce>> CountTransactionsAsync();
        


        Task<List<SumFeesTransactionsResponse>> SumFeesTransactionsAsync();
    }

    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository repository)
        {
            _reportsRepository = repository;
        }

        public async Task<List<CountTransRespoce>> CountTransactionsAsync() //+
        {
            var transactions = await _reportsRepository.GetTransactionsAsync();
            List<CountTransRespoce> result = new List<CountTransRespoce>();
            foreach (DatePeriod3 value in Enum.GetValues(typeof(DatePeriod3)).Cast<TransType>().OrderBy(v => (int)v))
            {
                var res = new CountTransRespoce();
                List<DataByPeriodCount> dbp = new List<DataByPeriodCount>();
                res.DatePeriod = Enum.GetName(typeof(DatePeriod), value);
                res.DataByPeriod = new List<DataByPeriodCount>();
                foreach (TransType transType in Enum.GetValues(typeof(TransType)))
                {
                    var currentResult = transactions
                        .Where(x => x.CreatedAt > DateTime.Now.AddMonths(-(int)value))
                        .Where(y => y.TransType == transType)
                        .Count();
                    var rr = new DataByPeriodCount()
                    {
                        TransactionsType = Enum.GetName(typeof(TransType), transType),
                        TransactionsQuantity = currentResult
                    };
                    res.DataByPeriod.Add(rr);
                }
                result.Add(res);
            }
            return result;
        }


        public async Task<List<SumFeesTransactionsResponse>> SumFeesTransactionsAsync()
        {
            var transactions = await _reportsRepository.GetTransactionsAsync();
            List<SumFeesTransactionsResponse> result = new List<SumFeesTransactionsResponse>();
            foreach (DatePeriod value in Enum.GetValues(typeof(DatePeriod)))
            {
                var res = new SumFeesTransactionsResponse();
                List<DataByPeriod> dbp = new List<DataByPeriod>();
                res.DatePeriod = Enum.GetName(typeof(DatePeriod), value);
                res.DataByPeriod = new List<DataByPeriod>();
                foreach (Currency currency in Enum.GetValues(typeof(Currency)))
                {
                    var currentResult = transactions
                        .Where(x => x.CreatedAt > DateTime.Now.AddMonths(-(int)value))
                        .Where(y => y.CurrencyFrom == currency)
                        .Sum(s => s.Fee);
                    var rr = new DataByPeriod()
                    {
                            Currency = Enum.GetName(typeof(Currency), currency),
                            sumOfFees = currentResult
                    };
                    res.DataByPeriod.Add(rr);
                }
                result.Add(res);
            }
            return result;
        }

        // სტატისტიკა 1 
        public async Task<List<JsonUserStatisticResponse>> ViewRegistredCustomersStatistic()
        {
            int DaysBeforeNewYear = (DateTime.Now - (new DateTime(DateTime.Now.Year, 1, 1))).Days;

            List<int> collection = new List<int> { 365, DaysBeforeNewYear, 30 };
            List<JsonUserStatisticResponse> result = new List<JsonUserStatisticResponse>();
            var usersInRole = await _reportsRepository.GetUsersInRoleUserAsync();

            foreach (var item in collection)
            {
                var elem = new JsonUserStatisticResponse()
                {
                    DatePeriod = $"Last {item} days",
                    NumberOfUsers = usersInRole.Where(d => d.RegisteredAt > DateTime.Now.AddDays(-item) && d.RegisteredAt < DateTime.Now).Count()
                };
                result.Add(elem);
            }
            return result;
        }

        //public async Task<List<CountTransactionsResponse>> JsonCountTtransactionsStatisticAsync()
        //{
        //    List<CountTransactionsResponse> result = new List<CountTransactionsResponse>();
        //    var transactions = await _reportsRepository.GetTransactionsAsync();

        //    foreach (DatePeriod value in Enum.GetValues(typeof(DatePeriod)).Cast<DatePeriod>().OrderBy(v => (int)v))
        //    {
        //        CountTransactionsResponse countT = new CountTransactionsResponse();
        //        countT.DatePeriod = value;
        //        countT.DataByPeriod = new DataByPeriod();

        //        foreach (TransType trans in Enum.GetValues(typeof(TransType)).Cast<TransType>().OrderBy(v => (int)v))
        //        {
        //            countT.DataByPeriod.TransType = trans;
        //            countT.DataByPeriod.DataByType = new DataByType();
        //            foreach (Currency currency in Enum.GetValues(typeof(Currency)).Cast<TransType>().OrderBy(v => (int)v))
        //            {
        //                countT.DataByPeriod.DataByType.Currency = currency;
        //                countT.DataByPeriod.DataByType.DataByCurency.countData = transactions
        //                    .Where(x => x.CreatedAt > DateTime.Now.AddMonths(-20))
        //                    //.Where(x => x.CurrencyFrom == currency)
        //                    //.Where(x => x.TransType == trans)
        //                    .Count();
        //            }
        //        }
        //        result.Add(countT);
        //    }
        //    return result;
        //}


    }
}