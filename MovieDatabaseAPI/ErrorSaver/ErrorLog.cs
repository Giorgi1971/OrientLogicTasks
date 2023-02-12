using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.ErrorSaver
{
    public class ErrorLog
    {
        [Key]
        public int ErrorLogId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}
