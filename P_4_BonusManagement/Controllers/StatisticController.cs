using System;
using Microsoft.AspNetCore.Mvc;
using P_4_BonusManagement.Data.Entity;
using P_4_BonusManagement.Repositories;

namespace P_4_BonusManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StatisticController : ControllerBase
    {

        private readonly IStatisticRepository _statisticRepository;

        public StatisticController(IStatisticRepository opt)
        {
            _statisticRepository = opt;
        }

        [HttpGet("Top-bonused-employee")]
        public async Task<ActionResult<List<EmployeeEntity>>> GetTopEmployeesWithMostBonuses()
        {
            try
            {
                return Ok(await _statisticRepository.GetTopEmployeesWithMostBonuses());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }

        [HttpGet("Top-10-recomendator")]
        public async Task<ActionResult<List<NClass>>> GetTopRecomendator()
        {
            try
            {
                var result = await _statisticRepository.GetTopRecomendator();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }

    }
}

