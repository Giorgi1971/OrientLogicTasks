using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPACalculatorAPI.Db.Entity
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }
        public string PersonalNubmer { get; set; }
        public string Course { get; set; }
    }
}
