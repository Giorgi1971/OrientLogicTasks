using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Services;
using CredoProject.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        private readonly CredoDbContext _db;

        public OperatorController(ICoreServices coreServices, CredoDbContext db)
        {
            _db = db;
            _coreServices = coreServices;
        }

        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<UserEntity>> RegisterCustomerAsync([FromBody] CreateCustomerRequest request)
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

        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsync([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok(account);
        }

        [Authorize("ApiCustomer", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard-customer")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsyncCustomer([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetCustomerEntity(int id)
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
