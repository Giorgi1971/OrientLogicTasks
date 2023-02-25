using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_6_Pricing_API.Data;
using P_6_Pricing_API.Data.Entity;
using P_6_Pricing_API.Models.Requests;
using P_6_Pricing_API.Service;
using P_6_Pricing_API.Repository;

namespace P_6_Pricing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly PricingDbContext _context;
        private readonly ICalculateRepository _calcRepo;

        public UserDataController(PricingDbContext context, ICalculateRepository calculate)
        {
            _context = context;
            _calcRepo = calculate;
        }

        // POST: api/UserData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("calculate")]
        public async Task<ActionResult<UserInput>> PostUserInput([FromBody] UserInputRequest request)
        {
            if (_context.UserInputs == null)
            {
                return Problem("Entity set 'PricingDbContext.UserInputs'  is null.");
            }
            var result = await _calcRepo.CalculateEndingBalance(request);

            return Ok(result);
        }

        // GET: api/UserData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInput>>> GetUserInputs()
        {
          if (_context.UserInputs == null)
          {
              return NotFound();
          }
            return await _context.UserInputs.ToListAsync();
        }

        // POST: api/UserData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-input")]
        public async Task<ActionResult<UserInput>> PostUserInputResult([FromBody]UserInputRequest request)
        {
            if (_context.UserInputs == null)
            {
                return Problem("Entity set 'PricingDbContext.UserInputs'  is null.");
            }
            UserInput userInput = new()
            {
                UserId = request.UserId,
                Balance = request.Balance,
                InterestType = request.InterestType,
                ProductType = request.ProductType,
                PaymentType = request.PaymentType,
                OriginalMonth = request.OriginalMonth,
                AvgMonthlyFeeIncome = request.AvgMonthlyFeeIncome,
                CommitmentAmount = request.CommitmentAmount,
                DiaxountFromStandardFee = request.DiaxountFromStandardFee,
                InterestRate = request.InterestRate,
                InterestSpread = request.InterestSpread,
                MonthlyFeeIncome = request.MonthlyFeeIncome,
                TeaserPeriod = request.TeaserPeriod,
                TeaserSpread = request.TeaserSpread
            };
            await _context.UserInputs.AddAsync(userInput);
            await _context.SaveChangesAsync();

            return Ok(userInput);
        }
    }
}
