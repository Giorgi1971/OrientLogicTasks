using System;
using System.ComponentModel.DataAnnotations;

namespace P_4_BonusManagement.Data.Entity
{
    public class EmployeeEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 2)]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public int PersonalNumber { get; set; }
        public double Salary { get; set; }
        public int RecommenderId { get; set; }
        public DateTime HiringDate { get; set; }

        public List<BonusEntity> Bonuses { get; set; }
    }
}


