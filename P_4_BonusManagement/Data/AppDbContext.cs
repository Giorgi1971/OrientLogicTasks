using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Data.Entity;

namespace P_4_BonusManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) {}

        public DbSet<EmployeeEntity> EmployeeEntities { get; set; }
        public DbSet<BonusEntity> BonusEntities { get; set; }
    }
}

