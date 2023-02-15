using System;
using TodoApp.Api.Db.Entity;

namespace TodoApp.Api.Models.Requests
{
    public class ChangeTodoStatusRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TodoStatus Status { get; set; }
    }
}

