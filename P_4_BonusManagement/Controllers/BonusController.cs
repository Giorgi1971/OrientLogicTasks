using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P_4_BonusManagement.Data.Entity;
using P_4_BonusManagement.Models.Requests;
using P_4_BonusManagement.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P_4_BonusManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        private readonly IBonusRepository _bonusRepository;

        public BonusController(IBonusRepository b)
        {
            _bonusRepository = b;
        }

        [HttpGet("All-Bonuses")]
        // როდის ჭირდება აქშენრეზალტში ენტიტის ჩასმა???
        public async Task<ActionResult> GetBonusesAsync()
        {
            try
            {
                return Ok(await _bonusRepository.GetBonusesAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }

        [HttpPost("search-bonuses")]
        // როდის ჭირდება აქშენრეზალტში ენტიტის ჩასმა???
        public async Task<ActionResult> GetBonusesByDateAsync([FromBody]SearchBonusByDateRequest request)
        {
            try
            {
                return Ok(await _bonusRepository.SearchBonusesByDateAsync(request));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }
    }
}

