using System;
using Microsoft.AspNetCore.Mvc;
using P_4_BonusManagement.Data;
using P_4_BonusManagement.Data.Entity;
using P_4_BonusManagement.Repositories;
using P_4_BonusManagement.Services;

namespace P_4_BonusManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StatisticController : ControllerBase
    {

        private readonly IStatisticRepository _statisticRepository;
        private readonly ICalculateStatistic _calculateStatistic;
        private readonly AppDbContext _db;

        public StatisticController(IStatisticRepository opt, ICalculateStatistic calc, AppDbContext db)
        {
            _statisticRepository = opt;
            _calculateStatistic = calc;
            _db = db;
        }

        [HttpGet("top-10-bonused-by-calculate")]
        public async Task<ActionResult> CalculateTopBonused()
        {
            try
            {
                var _employees = _db.EmployeeEntities.ToList();
                var _bonuses = _db.BonusEntities.ToList();

                var result = await _calculateStatistic.CalculateTopBonused(_employees, _bonuses);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }

        [HttpGet("top-10-recomendator-by-calculate")]
        public async Task<ActionResult> CalculateTopRecomendators()
        {
            try
            {
                var _employees = _db.EmployeeEntities.ToList();
                var _bonuses = _db.BonusEntities.ToList();

                var result = await _calculateStatistic.CalculateTopRecomendator(_employees, _bonuses);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
        }

        [HttpGet("Top-10-recomendator-count")]
        public async Task<ActionResult> GetTopRecomendatorCount2()
        {
            try
            {
                var query = _statisticRepository.GetJoinedData();

                var result = _calculateStatistic.GetTopRecomendatorCount(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Bonus Controller (All onuses), retrieving data from the database");
            }
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
    }
}

