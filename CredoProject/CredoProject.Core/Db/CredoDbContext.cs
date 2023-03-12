using System;
using Microsoft.EntityFrameworkCore;

using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Db
{
    public class CredoDbContext : DbContext
    {
        //public CredoDbContext(DbContextOptions<CredoDbContext> opt) : base(opt) { }
        public CredoDbContext(DbContextOptions<CredoDbContext> options)
        : base(options)
        {
        }
        public DbSet<CustomerEntity>? CustomerEntities { get; set; }
        public DbSet<AccountEntity>? AccountEntities { get; set; }
        public DbSet<OperatorEntity>? OperatorEntities { get; set; }
        //public DbSet<ErrorLogEntity> ErrorLogEntities { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BonusEntity>()
        //        .HasOne(p => p.EmployeeEntity)
        //        .WithMany(b => b.BonusEntities)
        //        .OnDelete(DeleteBehavior.Cascade);
        //.OnDelete(DeleteBehavior.Delete);
        //.OnDelete(DeleteBehavior.SetNull);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (
                    "Server = localhost; " +
                    "Database = CredoNewEntityDb; " +
                    "User Id=sa; " +
                    "Password=HardT0Gue$$Pa$$word; " +
                    //"TrustedConnection=true;" +
                    "Encrypt=False;"
                );
        }
    }
}

