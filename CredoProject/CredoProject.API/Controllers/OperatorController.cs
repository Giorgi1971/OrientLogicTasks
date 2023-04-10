using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Services;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        private readonly UserManager<UserEntity> _userManager;

        public OperatorController(ICoreServices coreServices, UserManager<UserEntity> userManager)
        {
            _coreServices = coreServices;
            _userManager = userManager;
        }

        [HttpPost("register-customer-by-operator")]
        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerRequest request)
        {
            var entity = new UserEntity()
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PersonalNumber = request.PersonalNumber,
                RegisteredAt = DateTime.Now
            };

            var hasherUser = new PasswordHasher<UserEntity>();
            entity.PasswordHash = hasherUser.HashPassword(entity, request.Password);

            // აქ request.Password სწორია?? თუ entity.PasswordHash
            var result = await _userManager.CreateAsync(entity, request.Password);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            await _userManager.AddToRoleAsync(user, request.Role);
            return Ok();
        }

        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateAcount")]
        public async Task<ActionResult> RegisterAccountAsync([FromBody] CreateAccountRequest request)
        {
            var account = await _coreServices.RegisterAccountAsync(request);
            return Ok(account);
        }

        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard")]
        public async Task<ActionResult> RegisterCardAsync([FromBody] CreateCardRequest request)
        {
            var account = await _coreServices.RegisterCardAsync(request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserEntity(int id)
        {
            var userEntity = await _coreServices.GetUserEntity(id);
            return Ok(userEntity);
        }
    }
}
