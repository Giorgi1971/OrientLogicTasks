using System;
namespace P_4_BonusManagement.Data.Entity
{
    public class BonusEntity
    {
        public int BonusEntityId { get; set; }
        public double BonusAmount { get; set; }
        public DateTime IssueDate { get; set; }

        public int EmployeeEntityId { get; set; }
        public EmployeeEntity EmployeeEntity { get; set; }
    }
}

