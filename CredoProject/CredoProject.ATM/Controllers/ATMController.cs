using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CredoProject.Core.Models.Requests.Card;
using CredoProject.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CredoProject.ATM.Controllers
{
    [Route("api/[controller]")]
    public class ATMController : ControllerBase
    {

        private readonly IATMServices _atmServices;

        public ATMController(IATMServices atmServices)
        {
            _atmServices = atmServices;
        }

        [HttpPost("card-authorization")]
        public async Task<string> CardAuthorizationAsync([FromBody] CardAutorizationRequest request)
        {
            var result = await _atmServices.CardAuthorizationAsync(request);
            return result;
        }

        [HttpPost("change-card's-pin")]
        public async Task<string> ChangeCardPinAsync([FromBody] ChangeCardPinRequest request)
        {
            var result = await _atmServices.ChangeCardPinAsync(request);
            return result;
        }

        [HttpPost("card-amount")]
        public async Task<string> GetCardBalanceAsync([FromBody] CardAutorizationRequest request)
        {
            var result = await _atmServices.GetCardBalanceAsync(request);
            return result;
        }

        [HttpPost("take-many")]
        public async Task<string> WithdrawManyFromCardAsync([FromBody] WithdrawManyFromCardRequest request)
        {
            var result = await _atmServices.WithdrawManyFromCardAsync(request);
            return result;
        }

    }
}

