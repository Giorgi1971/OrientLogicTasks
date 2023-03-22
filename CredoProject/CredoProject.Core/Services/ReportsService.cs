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
        Task<List<SumFeesTransactionsResponse>> SumFeesTransactionsAsync();
        Task<List<JsonUserStatisticResponse>> ViewRegistredCustomersStatistic();
        Task<List<CountTransRespoce>> CountTransactionsAsync();
        Task<List<TransFeesAverageResponse>> AverageFeeByTransactionsAsync();
    }

    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository repository)
        {
            _reportsRepository = repository;
        }

        public async Task<List<TransFeesAverageResponse>> AverageFeeByTransactionsAsync()
        {
            var transactions = await _reportsRepository.GetTransactionsAsync();
            List<TransFeesAverageResponse> result = new List<TransFeesAverageResponse>();
            foreach (TransType transType in Enum.GetValues(typeof(TransType)))
            {
                var res = new TransFeesAverageResponse();
                res.TransactionType = Enum.GetName(typeof(TransType), transType);
                res.DataByPeriod = new List<DataByPeriodAverage>();

                var currentResultAll = transactions
                    .Where(x => x.TransType == transType)
                    .Average(s => s.Fee);
                var rrAll = new DataByPeriodAverage()
                {
                    AverageOfFees = currentResultAll,
                    Currency = "AllInGel"
                };
                res.DataByPeriod.Add(rrAll);

                foreach (Currency currency in Enum.GetValues(typeof(Currency)))
                {
                    var currentResult = transactions
                        .Where(x => x.TransType == transType)
                        .Where(y => y.CurrencyFrom == currency)
                        .Average(s => s.Fee);
                    var rr = new DataByPeriodAverage()
                    {
                        AverageOfFees = currentResult,
                        Currency = Enum.GetName(typeof(Currency), currency)
                    };
                    res.DataByPeriod.Add(rr);
                }
                result.Add(res);
            }
            return result;
        }


        public async Task<List<CountTransRespoce>> CountTransactionsAsync()
        {
            var transactions = await _reportsRepository.GetTransactionsAsync();
            List<CountTransRespoce> result = new List<CountTransRespoce>();
            foreach (DatePeriod3 value in Enum.GetValues(typeof(DatePeriod3)).Cast<TransType>().OrderBy(v => (int)v))
            {
                var res = new CountTransRespoce();
                res.DatePeriod = Enum.GetName(typeof(DatePeriod), value);
                res.DataByPeriod = new List<DataByPeriodCount>();
                var currentResultAll = transactions
                    .Where(x => x.CreatedAt > DateTime.Now.AddMonths(-(int)value))
                    .Count();

                var rrAll = new DataByPeriodCount()
                {
                    TransactionsType = "All",
                    TransactionsQuantity = currentResultAll
                };
                res.DataByPeriod.Add(rrAll);

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
                //List<DataByPeriod> dbp = new List<DataByPeriod>();
                res.DatePeriod = Enum.GetName(typeof(DatePeriod), value);
                res.DataByPeriod = new List<DataByPeriod>();

                var currentResultAll = transactions
                    .Where(x => x.CreatedAt > DateTime.Now.AddMonths(-(int)value))
                    .Sum(s => s.Fee);
                var rrAll = new DataByPeriod()
                {
                    Currency = "AllInGel",
                    sumOfFees = currentResultAll
                };
                res.DataByPeriod.Add(rrAll);

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
    }
}