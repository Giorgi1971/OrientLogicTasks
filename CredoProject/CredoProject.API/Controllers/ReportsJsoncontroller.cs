using System;
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
    public class ReportsJsonController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly UserManager<UserEntity> _userManager;

        public ReportsJsonController(IReportsService reportsService, UserManager<UserEntity> userManager)
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
            return new JsonResult(transactions); ;
        }

        //[Authorize("ApiManager", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-bank-fees-from-transaction-by-period")]
        public async Task<ActionResult> SumFeesTransactionsAsync()
        {
            var transactions = await _reportsService.SumFeesTransactionsAsync();
            return new JsonResult(transactions); ;
        }
    }
}

