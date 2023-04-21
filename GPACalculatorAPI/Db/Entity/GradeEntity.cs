using System;
using System.ComponentModel.DataAnnotations;

namespace GPACalculatorAPI.Db.Entity
{
    public class GradeEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int Score { get; set; }
    }
}
