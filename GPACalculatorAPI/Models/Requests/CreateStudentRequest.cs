using System;
using System.ComponentModel.DataAnnotations;

namespace GPACalculatorAPI.Models.Requests
{
    public class CreateStudentRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must not be more than 200 characters or Lees 3!")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNubmer { get; set; }
        public string Course { get; set; }
    }
}

