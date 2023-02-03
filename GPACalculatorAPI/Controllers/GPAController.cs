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


        [HttpPost("CreateStudent")]
        public async Task<ActionResult<StudentEntity>> CreateStudent([FromBody]CreateStudentRequest request)
        {
            var createStudent = _studentRepositor.CreateStudenAsync(request);
            await _studentRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }


        [HttpPost("CreateSubject")]
        public async Task<ActionResult<StudentEntity>> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            var createStudent = _subjectRepositor.CreateSubjectAsync(request);
            await _subjectRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }


        [HttpPost("Student/{StudentId}/CreateGrades")]
        public async Task<ActionResult<GradeEntity>> CreateGradeAsync(int StudentId, [FromBody] CreateGradeRequest request)
        {
            var createGrade = _gradeRepositor.CreateGradeAsync(StudentId, request);
            await _studentRepositor.SaveChangesAsync();
            return Ok(createGrade);
        }


        [HttpGet("student/{studentId}/grades")]
        public async Task<ActionResult<GradeEntity>> GetStudentGrades(int studentId)
        {
            var cetGrades = await _gradeRepositor.GetStudentGrades(studentId);
            return Ok(cetGrades);
        }


        [HttpGet("student/{studentId}/gpa")]
        public async Task<ActionResult<double>> GetStudentGPA(int studentId)
        {
            var cetGPA = await _gradeRepositor.GetStudentGPAAsync(studentId);
            return Ok(cetGPA);
        }


        [HttpGet("top-3-subject")]
        public async Task<ActionResult<List<SubjectEntity>>> GetTop3Subject()
        {
            var getTop3Subject = await _gradeRepositor.GetTop3Subject();
            return Ok(getTop3Subject);
        }


        [HttpGet("Less-3-subject")]
        public async Task<ActionResult<List<SubjectEntity>>> GetBottom3Subject()
        {
            var getTop3Subject = await _gradeRepositor.GetBottom3Subject();
            return Ok(getTop3Subject);
        }


        [HttpGet("Top-10-Students-byGPA")]
        public async Task<ActionResult<List<StudentEntity>>> GetTop10StudentByGPA()
        {
            var getTop10StudentByGPA = await _gradeRepositor.GetTop10StudentByGPA();
            return Ok(getTop10StudentByGPA);
        }
    }
}
