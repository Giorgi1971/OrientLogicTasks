using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Responses;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Models.Requests.Customer;
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
        [HttpGet("view-your-accounts")]
        public async Task<ActionResult<List<CustomerAccountsResponse>>> GetUserAccountsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var accounts = await _coreServices.GetUserAccounts(user.Id);
            return accounts;
        }

        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("view-your-cards")]
        public async Task<ActionResult<List<CardsResponse>>> GetUserCardsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var accounts = await _coreServices.GetUserCardsAsync(user.Id);
            return accounts;
        }

        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("transfer-money-atm")]
        public async Task<ActionResult<string>> TransferMonnyInnerAsync([FromBody]TransferRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _coreServices.TransferMonnyInnerAsync(request, user.Id);
            var accounts = await _coreServices.GetUserCardsAsync(user.Id);
            return result;
        }
    }
}
