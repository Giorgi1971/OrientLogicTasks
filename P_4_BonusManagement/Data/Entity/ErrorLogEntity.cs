using System;
using System.ComponentModel.DataAnnotations;

namespace P_4_BonusManagement.Data.Entity
{
    public class ErrorLogEntity
    {
        [Key]
        public int ErrorLogId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}

