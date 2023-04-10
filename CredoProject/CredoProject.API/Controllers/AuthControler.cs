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
        //private readonly RoleManager<RoleEntity> _roleManager;

        public AuthController(
            UserManager<UserEntity> userManager,
            //RoleManager<RoleEntity> roleManager,
            TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToString());
            if (user == null)
                return NotFound("Invalid email or password!!!");

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCorrectPassword)
                return BadRequest("Invalid email or password");
            var role = _userManager.GetRolesAsync(user).Result[0];

            return Ok(_tokenGenerator.Generate(user.Id, role));
        }
    }
}
