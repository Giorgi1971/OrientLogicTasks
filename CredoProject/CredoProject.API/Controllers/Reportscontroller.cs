﻿using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Responses;
using CredoProject.Core.Models.Responses.ReportsResponce;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Models.Requests.Customer;
using CredoProject.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly UserManager<UserEntity> _userManager;

        public ReportsController(IReportsService reportsService, UserManager<UserEntity> userManager)
        {
            _reportsService = reportsService;
            _userManager = userManager;
        }

        //[Authorize("ApiManager", AuthenticationSchemes = "Bearer")]
        [HttpGet("view-registred-customers-statistic")]
        public async Task<ActionResult> ViewRegistredCustomersStatistic()
        {
            var users = await _reportsService.ViewRegistredCustomersStatistic();
            return new JsonResult(users);
        }

        //[Authorize("ApiManager", AuthenticationSchemes = "Bearer")]
        [HttpGet("count-transactions-by-period")]
        public async Task<ActionResult> CountTransactionsAsync()
        {
            var transactions = await _reportsService.CountTransactionsAsync();
            return new JsonResult(transactions);
        }

        //[Authorize("ApiManager", AuthenticationSchemes = "Bearer")]
        [HttpGet("sum-bank-fees-by-period")]
        public async Task<ActionResult> SumFeesTransactionsAsync()
        {
            var transactions = await _reportsService.SumFeesTransactionsAsync();
            return new JsonResult(transactions);
        }

        //[Authorize("ApiManager", AuthenticationSchemes = "Bearer")]
        [HttpGet("Average-fee-by-transactions")]
        public async Task<ActionResult> AverageFeeByTransactionsAsync()
        {
            var transactions = await _reportsService.AverageFeeByTransactionsAsync();
            return new JsonResult(transactions);
        }

        [HttpGet("ATM-total-sum")]
        public async Task<ActionResult> ATMTotalSum()
        {
            var transactions = await _reportsService.ATMTotalSum();
            return new JsonResult(transactions);
        }

        [HttpGet("Last-30-day-count-transactions")]
        public async Task<ActionResult> LastMonthTransactions()
        {
            var transactions = await _reportsService.LastMonthTransactions();
            return new JsonResult(transactions);
        }
    }
}

