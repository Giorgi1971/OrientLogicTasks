using System;
using System.ComponentModel.DataAnnotations;

namespace P_4_BonusManagement.Data.Entity
{
    public class EmployeeEntity
    {
        public int EmployeeEntityId { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        public int PersonalNumber { get; set; }
        public double Salary { get; set; }
        public int RecommenderId { get; set; }
        public DateTime HiringDate { get; set; }

        public List<BonusEntity> BonusEntities { get; set; }
    }
}

