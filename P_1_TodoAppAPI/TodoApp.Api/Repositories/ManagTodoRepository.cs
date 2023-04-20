using System;
using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Api.Auth;
using TodoApp.Api.Models.Requests;
using TodoApp.Api.Db.Entity;
using TodoApp.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Api.Repositories
{
    public interface IManagTodoRepository
    {
        Task<ToDoEntity?> ChangeTodoStatusAsync(int userId, ChangeTodoStatusRequest request);
        Task<List<ToDoEntity>> ListOfTodosAuthUserAsync(int Id);
        Task<List<ToDoEntity>> SearchTodoByTitleDescAsync(SearchTodoRequest request);
    }

    public class ManagTodoRepository : IManagTodoRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<UserEntity> _userManager;

        public ManagTodoRepository(AppDbContext db, UserManager<UserEntity> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ToDoEntity?> ChangeTodoStatusAsync(int userId, ChangeTodoStatusRequest request)
        {
            // Todo აქ ქვედა აუცილებელია და არ მიშვებს ??? ისე ნებისმიერ რიცხვს ვწერ ბაზაში
            //if(request.Status != TodoStatus.New || request.Status != TodoStatus.Done || request.Status != TodoStatus.Canceled)
            //if(request.Status != 0 || request.Status != 1 || request.Status != 2)
                //throw new Exception("request Status is not valid");
            var StatusChangedTodo = _db.Todos.FirstOrDefault(x => x.Id == request.Id);
            if (StatusChangedTodo == null)
                throw new Exception("This is not your Todo 1111111");
            if (StatusChangedTodo.UserId != userId)
                throw new Exception("This is not your Todo");
            StatusChangedTodo.Status = request.Status;
            await _db.SaveChangesAsync();
            return StatusChangedTodo;
        }

        public async Task<List<ToDoEntity>> ListOfTodosAuthUserAsync(int Id)
        {
            var listUserTodos = await _db.Todos
                .Where(x => x.UserId == Id)
                //.Where(z => z.Status == TodoStatus.New)
                .OrderByDescending(m => m.DeadLine)
                .ToListAsync();
            if (listUserTodos == null)
                // aq ra unda davabruno?
                throw new Exception("Authorize user have No ToDos");
            return listUserTodos;
        }

        public async Task<List<ToDoEntity>> SearchTodoByTitleDescAsync(SearchTodoRequest request)
        {
            var listUserTodos = await _db.Todos
                .Where(
                x =>
                x.Title.Contains(request.Title) ||
                x.Description.Contains(request.Description)
                )
                .OrderBy(m => m.Title)
                .ToListAsync();
            if (listUserTodos == null)
                // aq ra unda davabruno?
                throw new Exception("Authorize user have No ToDos");
            return listUserTodos;
        }
    }
}

