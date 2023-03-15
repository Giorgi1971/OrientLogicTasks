using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        private readonly CredoDbContext _db;

        public CustomerController(ICoreServices coreServices, CredoDbContext db)
        {
            _db = db;
            _coreServices = coreServices;
        }

        //[HttpPost("get-own-accounts")]
        //public async Task<ActionResult<List<AccountEntity>>> GetOwnAccounts()
        //{
        //    var customer = await _coreServices.RegisterCustomerAsync();
        //    return CreatedAtAction("GetCustomerEntity", new { id = customer.CustomerEntityId }, customer);
        //}
    }
}
