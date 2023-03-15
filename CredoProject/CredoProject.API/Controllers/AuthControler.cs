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
            // TODO:Check user credentials...
            var user = await _userManager.FindByIdAsync(request.Email);
            if (user == null)
                return NotFound("Invalid email or password!!!");

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCorrectPassword)
                return BadRequest("Invalid email or password");

            return Ok(_tokenGenerator.Generate(user.Email.ToString()));
        }

        // TODO:register
        [HttpPost("register-customer")]
        [Authorize("Operator", AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> Register([FromBody]CreateCustomerRequest request)
        {
            // Create
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

            //await _userManager.AddToRoleAsync(entity, "operator");
            var result = await _userManager.CreateAsync(entity, request.Password);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            await _userManager.AddToRoleAsync(user, "Operator");
            var dd = await _userManager.GetRolesAsync(user);
            Console.WriteLine(dd);
            return Ok();
        }

        // TODO: - I RegisterPasswordReset
        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody]RequestPasswordResetRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return NotFound("Use Not Found");
            // 1 Generate password reset Token
            var token = _userManager.GeneratePasswordResetTokenAsync(user);

            // 2 Insert email into SendEmailRequest table
            var sendEmailRequestEntity = new SendEmailRequestEntity();
            sendEmailRequestEntity.ToAddress = request.Email;
            sendEmailRequestEntity.Status = SendEmailRequestStatus.New;
            sendEmailRequestEntity.CreateAt = DateTime.Now;

            var url = _configuration["PasswordResetUrl"]!
                .Replace("{UserId}", user.Id.ToString())
                // აქ ჩემთან ToStrings ითხოვს.
                .Replace("{token}", token.ToString());

            var resetUrl = $"<a href =\"{url}\">Reset Password</a>";
            sendEmailRequestEntity.Body = $"Plaese, Reset Your Password: {resetUrl}";

            _sendEmailRequestRepository.Insert(sendEmailRequestEntity);
            await _sendEmailRequestRepository.SaveChangesAsync();
            // 3 Return result
            return Ok();
        }

        // TODO: - II ResetPassword
        [HttpPost("reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound("user Not Found.");
            var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!resetResult.Succeeded)
            {
                var firstError = resetResult.Errors.First();
                return StatusCode(500, firstError.Description);
            }
            // 1 validate Token
            // 2 validate new Password
            // 3 Reset Password
            // 4 Return result
            return Ok();
        }
    }
}
