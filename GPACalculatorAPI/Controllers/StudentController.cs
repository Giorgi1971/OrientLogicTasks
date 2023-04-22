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

    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly IGradeRepository _gradeRepositor;

        public StudentController(StudentService studentService, IGradeRepository gradeRepository)
        {
            _studentService = studentService;
            _gradeRepositor = gradeRepository;
        }

        [HttpPost()]
        public async Task<ActionResult> CreateStudentAsync([FromBody] CreateStudentRequest request)
        {
            var createStudent = await _studentService.CreateStudentAsync(request);
            await _studentService.SaveChangesAsync();
            return Ok(createStudent);
        }

        [HttpPost("{StudentId}/Grade")]
        public async Task<ActionResult> CreateGradeAsync(int StudentId, [FromBody] CreateGradeRequest request)
        {
            var createGrade = await _studentService.CreateGradeAsync(StudentId, request);
            await _studentService.SaveChangesAsync();
            return Ok(createGrade);
        }

        [HttpGet("{studentId}/Grades")]
        public async Task<ActionResult<List<GradeEntity>>> GetStudentGradesAsync(int studentId)
        {
            var getGrades = await _studentService.GetStudentGradesAsync(studentId);
            return Ok(getGrades);
        }

        [HttpGet("{studentId}/GPA")]
        public async Task<ActionResult> GetStudentGPAAsync(int studentId)
        {
            var getGPA = await _studentService.GetStudentGPAAsync(studentId);
            return Ok(getGPA);
        }
    }
}
