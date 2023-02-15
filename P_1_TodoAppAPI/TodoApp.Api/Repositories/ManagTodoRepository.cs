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


namespace TodoApp.Api.Repositories
{
    public interface IManagTodoRepository
    {
        Task<ToDoEntity?> ChangeTodoStatusAsync(int userId, ChangeTodoStatusRequest request);
        Task<List<ToDoEntity>> ListOfTodosAuthUserAsync(int Id);
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
            var StatusChangedTodo = _db.Todos.FirstOrDefault(x => x.Id == request.Id);
            if (StatusChangedTodo == null)
                throw new Exception("This is not your Todo 1111111");
            if (StatusChangedTodo.UserId != userId)
                throw new Exception("This is not your Todo");
            StatusChangedTodo.Status = request.Status;
            await _db.SaveChangesAsync();
            return StatusChangedTodo;
        }

        // ამას ასინქრონულს ვერ ვაკეთებ
        public async Task<List<ToDoEntity>> ListOfTodosAuthUserAsync(int Id)
        {
            var listUserTodos = _db.Todos
                .Where(x => x.UserId == Id)
                .OrderByDescending(m => m.DeadLine)
                .ToList();
            if (listUserTodos == null)
                // aq ra unda davabruno?
                throw new Exception("Authorize user have No ToDos");
            return listUserTodos;
        }
    }
}

