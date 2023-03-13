using System;
using Microsoft.EntityFrameworkCore;

using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CredoProject.Core.Db
{
    public class CredoDbContext : DbContext
    {
        //public CredoDbContext(DbContextOptions<CredoDbContext> opt) : base(opt) { }
        public CredoDbContext(DbContextOptions<CredoDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<AccountEntity> AccountEntities { get; set; }
        public DbSet<OperatorEntity> OperatorEntities { get; set; }
        public DbSet<CardEntity> CardEntities { get; set; }
        public DbSet<TransactionEntity> TransactionEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEntity>()
                .HasKey(sc => new { sc.AccountFromId, sc.AccountToId });

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(sc => sc.AccountEntityFrom)
                .WithMany(s => s.FromTransactionEntities)
                .HasForeignKey(sc => sc.AccountFromId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(sc => sc.AccountEntityTo)
                .WithMany(c => c.ToTransactionEntities)
                .HasForeignKey(sc => sc.AccountToId)
                .OnDelete(DeleteBehavior.NoAction);


            //modelBuilder.Entity<AccountEntity>()
            //    .HasMany(p => p.FromTransactionEntities)
            //    .WithOne(b => b.AccountEntityFrom)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<AccountEntity>()
            //    .HasMany(p => p.ToTransactionEntities)
            //    .WithOne(b => b.AccountEntityTo)
            //    .OnDelete(DeleteBehavior.Cascade);

            //.OnDelete(DeleteBehavior.Delete);
            //.OnDelete(DeleteBehavior.SetNull);
        }

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

