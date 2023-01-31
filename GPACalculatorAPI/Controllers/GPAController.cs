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
    public class GPAController : ControllerBase
    {
        private readonly IStudentRepositor _studentRepositor;

        public GPAController(IStudentRepositor movieRepository)
        {
            _studentRepositor = movieRepository;
        }


    [Route("api/[controller]")]

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("Create")]
        public async Task<ActionResult<StudentEntity>> CreateStudent([FromBody]CreateStudentRequest request)
        {
            var createStudent = _studentRepositor.CreateStudenAsync(request);
            await _studentRepositor.SaveChangesAsync();
            return Ok(createStudent);
        }


    }
}

