using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Auth;
using TodoApp.Api.Models.Requests;
using TodoApp.Api.Db.Entity;
using TodoApp.Api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TodoController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITodoRepository _todoRepository;

        public TodoController(
            UserManager<UserEntity> userManager,
            ITodoRepository todoRepository
            )
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
        }

        // მუშაობს ავტორიზაციის გარეშე
        //[Authorize]
        //[Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("All-ToDos")]
        public async Task<ActionResult> GetTodos()
        {
            //try
            //{
                return Ok(await _todoRepository.GetTodos());
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Error retrieving data from the database");
            //}
        }


        //[Authorize]
        // მუშაობს ავტორიზაციის გარეშე
        // GET api/values/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ToDoEntity>> GetToDo(int id)
        {
            try
            {
                var result = await _todoRepository.GetToDo(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // მუშაობს ავტორიზაციის გარეშე
        // POST api/values
        //[Authorize]
        //[Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("create")]
        public async Task<ActionResult<ToDoEntity>> CreateToDo([FromBody] CreateTodoRequest request)
        {
            var user = await _todoRepository.GetUserFromRepositoryAsync(5);
            //var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not Found");

            var result = await _todoRepository.AddTodoWithInsertAsync(user.Id, request.Title, request.Decsription, request.Deadline);
            await _todoRepository.SaveChangesAsync();
            return Ok(result);
        }


        // მუშაობს ავტორიზაციის გარეშე
        // PUT api/values/5
        [HttpPut("update")]
        public async Task<ActionResult<ToDoEntity>> UpdateEmployee([FromBody] UpdateTodoRequest request)
        {
            try
            {
                var user = await _todoRepository.GetUserFromRepositoryAsync(5);
                //var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("User (Finding without Id) not Found");

                var userId = user.Id;

                var todoToUpdate = await _todoRepository.GetToDo(request.Id);
                if (todoToUpdate == null)
                    return NotFound($"Employee with Id = {request.Id} not found");

                if (userId != todoToUpdate.UserId)
                    return BadRequest("Employee ID mismatch");

                return await _todoRepository.UpdateTodo(request.Id, request.Title, request.Decsription, request.Deadline);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteToDoEntity(int id)
        {
            try
            {
                await _todoRepository.DeleteTodo(id);
                await _todoRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}

