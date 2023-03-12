using System;
using Microsoft.EntityFrameworkCore;

namespace BankCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        { }


        public DbSet<Customer> Customers { get; set; }

    }
}

