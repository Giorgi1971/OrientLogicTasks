using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CredoProject.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICoreServices _coreServices;
        private readonly UserManager<UserEntity> _userManager;


        public CustomerController(ICoreServices coreServices, UserManager<UserEntity> userManager)
        {
            _coreServices = coreServices;
            _userManager = userManager;
        }

        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("get-own-accounts")]
        public async Task<ActionResult<List<AccountEntity>>> GetUserAccounts()
        {
            var test = User;
            var user = await _userManager.GetUserAsync(User);
            var accounts = await _coreServices.GetUserAccounts(user.Id);
            return accounts;
        }


    }
}
