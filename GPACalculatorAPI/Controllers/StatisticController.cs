using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPACalculatorAPI.Controllers;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using GPACalculatorAPI.Repositoreis;
using Microsoft.AspNetCore.Mvc;
using GPACalculatorAPI.Services;

namespace GPACalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StatisticController : ControllerBase
    {
        private readonly StatisticService _statisticService;

        public StatisticController(StatisticService gradeRepository)
        {
            _statisticService = gradeRepository;
        }

        [HttpGet("top-3-subject")]
        public async Task<ActionResult> GetTop3Subject()
        {
            var dd = await _statisticService.GetTop3SubjectAsync();
            return Ok(dd);
        }

        //[HttpGet("less-3-subject")]
        //public async Task<ActionResult<List<SubjectEntity>>> GetBottom3Subject()
        //{
        //    var getTop3Subject = await _statisticService.GetBottom3SubjectAsync();
        //    return Ok(getTop3Subject);
        //}

        //[HttpGet("top-10-Students-byGPA")]
        //public async Task<ActionResult<List<StudentEntity>>> GetTop10StudentByGPA()
        //{
        //    var getTop10StudentByGPA = await _statisticService.GetTop10StudentByGPAAsync();
        //    return Ok(getTop10StudentByGPA);
        //}
    }
}
