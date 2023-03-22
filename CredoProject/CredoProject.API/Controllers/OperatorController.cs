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

        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateAcount")]
        public async Task<ActionResult<AccountEntity>> RegisterAccountAsync([FromBody] CreateAccountRequest request)
        {
            var account = await _coreServices.RegisterAccountAsync(request);
            return Ok(account);
        }

        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateCard")]
        public async Task<ActionResult<CardEntity>> RegisterCardAsync([FromBody] CreateCardRequest request)
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

        [HttpPost("register-customer-by-operator")]
        [Authorize("ApiOperator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerRequest request)
        {
            request.Password = "pas123";
            request.PersonalNumber = "01020304057";
            // როლი უნდა შევამოწმო თუ არსებობს, რომ მერე ბაზაში უსერი როლის გარეშე არ დამრჩეს
            var entity = new UserEntity()
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PersonalNumber = request.PersonalNumber,
            };
            var hasherUser = new PasswordHasher<UserEntity>();
            entity.PasswordHash = hasherUser.HashPassword(entity, request.Password);

            var result = await _userManager.CreateAsync(entity, request.Password);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }
            // აქ არ თავიდან ბაზაში რომ არ ვეძებო უსერი, ისე აიდის ვერ გავიგებ???
            var user = await _userManager.FindByEmailAsync(request.Email);
            // ეს თუ ვერ ჩაიწერა მომხმარებელი როლის გარეშე რჩება ჩაწერი RolBack ხომ არ მჭირდება???
            await _userManager.AddToRoleAsync(user, request.Role);
            return Ok();
        }

    }
}
