using System;
using P_4_BonusManagement.Data.Entity;
namespace P_4_BonusManagement.Models
{
    public class NewEmptyClass
    {
        public EmptyClass emptyClass { get; set; } = null!;
        public IQueryable<BonusEntity> bonusEntity { get; set; } = null!;
    }
}

