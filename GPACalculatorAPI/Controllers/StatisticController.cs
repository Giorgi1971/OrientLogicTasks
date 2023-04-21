using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPACalculatorAPI.Controllers;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using GPACalculatorAPI.Repositoreis;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StatisticController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepositor;

        public StatisticController(IGradeRepository gradeRepository)
        {
            _gradeRepositor = gradeRepository;
        }

        [HttpGet("top-3-subject")]
        public async Task<ActionResult<List<SubjectEntity>>> GetTop3Subject()
        {
            var getTop3Subject = await _gradeRepositor.GetTop3Subject();
            return Ok(getTop3Subject);
        }

        [HttpGet("less-3-subject")]
        public async Task<ActionResult<List<SubjectEntity>>> GetBottom3Subject()
        {
            var getTop3Subject = await _gradeRepositor.GetBottom3Subject();
            return Ok(getTop3Subject);
        }

        [HttpGet("top-10-Students-byGPA")]
        public async Task<ActionResult<List<StudentEntity>>> GetTop10StudentByGPA()
        {
            var getTop10StudentByGPA = await _gradeRepositor.GetTop10StudentByGPA();
            return Ok(getTop10StudentByGPA);
        }
    }
}
