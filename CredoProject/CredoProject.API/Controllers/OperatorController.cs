using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Services;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        //private readonly CredoDbContext _db;

        public OperatorController(ICoreServices coreServices)
        {
            //_db = db;
            _coreServices = coreServices;
        }

        //[Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateAcount")]
        public async Task<ActionResult<AccountEntity>> RegisterAccountAsync([FromBody] CreateAccountRequest request)
        {
            var account = await _coreServices.RegisterAccountAsync(request);
            return Ok(account);
        }

        //[Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsync([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok(account);
        }

        //[Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard-customer")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsyncCustomer([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetUserEntity(int id)
        {
            var userEntity = await _coreServices.GetUserEntity(id);
            return Ok(userEntity);
        }
    }
}
