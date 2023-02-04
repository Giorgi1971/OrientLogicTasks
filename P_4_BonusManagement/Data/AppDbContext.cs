using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Data.Entity;

namespace P_4_BonusManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<BonusEntity> Bonusies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Employee Table
            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 1,
                FirstName = "John1",
                LastName = "Hastings",
                PersonalNumber = 01030019691,
                HiringDate = DateTime.Now,
                Salary = 2000,
                RecommenderId = 0,
            });

            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 2,
                FirstName = "John2",
                LastName = "Hastings2",
                PersonalNumber = 01030019692,
                HiringDate = DateTime.Now,
                Salary = 1000,
                RecommenderId = 1,
            });

            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 3,
                FirstName = "John3",
                LastName = "Hastings3",
                PersonalNumber = 01030019693,
                HiringDate = DateTime.Now,
                Salary = 1000,
                RecommenderId = 2,
            });

            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 4,
                FirstName = "John4",
                LastName = "Hastings4",
                PersonalNumber = 01030019694,
                HiringDate = DateTime.Now,
                Salary = 1000,
                RecommenderId = 3,
            });

            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 5,
                FirstName = "John5",
                LastName = "Hastings5",
                PersonalNumber = 01030019695,
                HiringDate = DateTime.Now,
                Salary = 1000,
                RecommenderId = 4,
            });
            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 6,
                FirstName = "John5",
                LastName = "Hastings5",
                PersonalNumber = 01030019695,
                HiringDate = DateTime.Now,
                Salary = 1000,
                RecommenderId = 1,
            });

            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 7,
                FirstName = "John1",
                LastName = "Hastings",
                PersonalNumber = 01030019697,
                HiringDate = DateTime.Now,
                Salary = 2000,
                RecommenderId = 0,
            });
            //Seed Departments Table
            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 1,
                    EmployeeId = 1,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 01, 02)
                });

            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 2,
                    EmployeeId = 2,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 02, 02)
                });

            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 3,
                    EmployeeId = 3,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 03, 02)
                });

            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 4,
                    EmployeeId = 4,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 04, 02)
                });


            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 5,
                    EmployeeId = 5,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 05, 02)
                });


            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 6,
                    EmployeeId = 6,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 08, 02)
                });


            modelBuilder.Entity<BonusEntity>().HasData(
                new BonusEntity
                {
                    Id = 7,
                    EmployeeId = 1,
                    BonusAmount = 1000,
                    IssueDate = new DateTime(2020, 09, 02)
                });
        }
    }
}

