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

    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepositor;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepositor = subjectRepository;
        }

        [HttpPost("Subject")]
        public async Task<ActionResult> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            var createStudent = _subjectRepositor.CreateSubjectAsync(request);
            await _subjectRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }
    }
}
