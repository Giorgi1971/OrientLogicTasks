using System;
using System.ComponentModel.DataAnnotations;

namespace GPACalculatorAPI.Models.Requests
{
    public class CreateGradeRequest
    {
        public int SubjectId { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int Score { get; set; }
    }
}

