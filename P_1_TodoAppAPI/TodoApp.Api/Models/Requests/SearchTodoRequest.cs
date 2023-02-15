using System;
namespace TodoApp.Api.Models.Requests
{
    public class SearchTodoRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

