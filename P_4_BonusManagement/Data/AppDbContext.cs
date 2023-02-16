using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using P_4_BonusManagement.Data.Entity;
using Serilog;

namespace P_4_BonusManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) {}

        public DbSet<EmployeeEntity> EmployeeEntities { get; set; }
        public DbSet<BonusEntity> BonusEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BonusEntity>()
                .HasOne(p => p.EmployeeEntity)
                .WithMany(b => b.BonusEntities)
                .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.Delete);
                //.OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = "Server=(localdb)\\mssqllocaldb;Database=myapp;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    var logger = new LoggerConfiguration()
        //        .WriteTo.MSSqlServer(connectionString, "ErrorLog")
        //        .CreateLogger();

        //    optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddSerilog(logger)));
        //}

    }
}

