using System;
using GPACalculatorAPI.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace GPACalculatorAPI.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }
    }
}

