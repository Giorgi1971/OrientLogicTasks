using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Services;
using CredoProject.Core.Validations;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        //private readonly BankRepository _bankRepository;
        //private readonly Validate _validate;
        private readonly CredoDbContext _db;

        public BankingController(ICoreServices coreServices, CredoDbContext db)
        {
            _db = db;
            _coreServices = coreServices;
            //_bankRepository = bankRepository;
            //_validate = validate;
        }


        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<CustomerEntity>> RegisterCustomerAsync([FromBody] CreateCustomerRequest request)
        {
            var customer = await _coreServices.RegisterCustomerAsync(request);
            return CreatedAtAction("GetCustomerEntity", new { id = customer.Id }, customer);
        }

        [HttpPost("CreateAcount")]
        public async Task<ActionResult<AccountEntity>> RegisterAccountAsync([FromBody] CreateAccountRequest request)
        {
            var account = await _coreServices.RegisterAccountAsync(request);
            return Ok(account);
        }

        [HttpPost("CreateCard")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsync([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomerEntity(int id)
        {
            if (_db.CustomerEntities == null)
            {
                return NotFound();
            }
            var customerEntity = await _db.CustomerEntities.FindAsync(id);

            if (customerEntity == null)
            {
                return NotFound();
            }

            return customerEntity;
        }
    }
}

