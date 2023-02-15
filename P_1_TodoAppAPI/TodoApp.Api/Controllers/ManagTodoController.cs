using System;
using TodoApp.Api.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Repositories;
using TodoApp.Api.Db.Entity;
using Microsoft.AspNetCore.Authorization;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ManagTodoController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IManagTodoRepository _managtodoRepository;
        private readonly ITodoRepository _todoRepository;

        public ManagTodoController(
            UserManager<UserEntity> userManager,
            IManagTodoRepository managtodoRepository,
            ITodoRepository todoRepository
            )
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
            _managtodoRepository = managtodoRepository;
        }

        //[Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeTodoStatus([FromBody] ChangeTodoStatusRequest request)
        {
            var user = await _todoRepository.GetUserFromRepositoryAsync(5);
            //var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("User not found");
            var changedTodo = await _managtodoRepository.ChangeTodoStatusAsync(user.Id, request);
            await _todoRepository.SaveChangesAsync();
            if (changedTodo == null)
                return Ok("change todo is null");
            return Ok(changedTodo);
        }


        //[Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("autherize-user's-todo-list")]
        public async Task<IActionResult> ListOfTodosAuthUser()
        {
            var user = await _todoRepository.GetUserFromRepositoryAsync(5);
            //var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("No Autherization");
            var ListTodos = await _managtodoRepository.ListOfTodosAuthUserAsync(user.Id);
            await _todoRepository.SaveChangesAsync();
            if (ListTodos == null)
                return Ok("change yodo is null");
            return Ok(ListTodos);
        }

        //[Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("search-todo-in-title-description")]
        public async Task<IActionResult> SearchTodoByTitleDescAsync(SearchTodoRequest request)
        {
            var ListTodos = await _managtodoRepository.SearchTodoByTitleDescAsync(request);
            if (ListTodos == null)
                return Ok("Noting Found in this filter");
            return Ok(ListTodos);
        }
    }
}
