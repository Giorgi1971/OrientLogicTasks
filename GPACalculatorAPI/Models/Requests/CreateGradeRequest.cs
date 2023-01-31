using System;
using System.ComponentModel.DataAnnotations;

namespace GPACalculatorAPI.Models.Requests
{
    public class CreateGradeRequest
    {
        public int SubjectId { get; set; }
        public int Score { get; set; }
    }
}

