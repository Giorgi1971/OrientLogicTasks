using System;
namespace P_4_BonusManagement.Models.Requests
{
    public class CreateBonusRequest
    {
        public int EmployeeId { get; set; }
        public double BonusAmount { get; set; }
    }
}

