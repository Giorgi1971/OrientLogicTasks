using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using GPACalculatorAPI.Repositoreis;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GPACalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GPAController : ControllerBase
    {
        private readonly IStudentRepositor _studentRepositor;
        private readonly IGradeRepository _gradeRepositor;
        private readonly ISubjectRepository _subjectRepositor;

        public GPAController(IStudentRepositor movieRepository, ISubjectRepository subjectRepository, IGradeRepository gradeRepository)
        {
            _studentRepositor = movieRepository;
            _subjectRepositor = subjectRepository;
            _gradeRepositor = gradeRepository;
        }



        // POST api/values
        [HttpPost("CreateStudent")]
        public async Task<ActionResult<StudentEntity>> CreateStudent([FromBody]CreateStudentRequest request)
        {
            var createStudent = _studentRepositor.CreateStudenAsync(request);
            await _studentRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }


        // POST api/values
        [HttpPost("CreateSubject")]
        public async Task<ActionResult<StudentEntity>> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            var createStudent = _subjectRepositor.CreateSubjectAsync(request);
            await _subjectRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }

        // POST api/values
        [HttpPost("CreateGrade")]
        public async Task<ActionResult<GradeEntity>> CreateGrade([FromBody] CreateGradeRequest request)
        {
            var createGrade = _gradeRepositor.CreateGradeAsync(request);
            await _studentRepositor.SaveChangesAsync();
            return Ok(createGrade);
        }

    }
}

