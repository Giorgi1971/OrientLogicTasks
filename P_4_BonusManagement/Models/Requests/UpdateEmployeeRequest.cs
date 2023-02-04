using System;
namespace P_4_BonusManagement.Models.Requests
{
    public class UpdateEmployeeRequest
    {
        //public int Id { get; set; }
        public string? LastName { get; set; }
        public double Salary { get; set; }
        //public int RecommenderId { get; set; }
        public DateTime HiringDate { get; set; }
    }
}

