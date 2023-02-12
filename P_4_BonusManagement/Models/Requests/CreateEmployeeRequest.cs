using System;
namespace P_4_BonusManagement.Models.Requests
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public int PersonalNumber { get; set; }
        public double Salary { get; set; }
        public int RecommenderId { get; set; }
    }
}
