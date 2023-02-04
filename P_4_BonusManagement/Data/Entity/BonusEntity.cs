using System;
namespace P_4_BonusManagement.Data.Entity
{
    public class BonusEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public double BonusAmount { get; set; }
        public DateTime IssueDate { get; set; }
    }
}

