using Microsoft.AspNetCore.Mvc;
using CredoProject.API.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.AuthRequests;


namespace CredoProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ISendEmailRequestRepository _sendEmailRequestRepository;

        public AuthController(
            IConfiguration configuration,
            ISendEmailRequestRepository sendEmailRequestRepository,
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _sendEmailRequestRepository = sendEmailRequestRepository;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            request.Password = "pas123";
            var user = await _userManager.FindByEmailAsync(request.Email.ToString());
            if (user == null)
                return NotFound("Invalid email or password!!!");

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCorrectPassword)
                return BadRequest("Invalid email or password");
            var role = _userManager.GetRolesAsync(user).Result[0];

            return Ok(_tokenGenerator.Generate(user.Id, role));
        }

        [HttpPost("register-customer-by-operator")]
        [Authorize("ApiOperator", AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> Register([FromBody]CreateCustomerRequest request)
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
