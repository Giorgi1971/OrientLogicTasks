using System;
using Microsoft.EntityFrameworkCore;
using P_6_Pricing_API.Data.Entity;
//using System.Reflection;

namespace P_6_Pricing_API.Data
{
    public class PricingDbContext: DbContext
    {
        public PricingDbContext(DbContextOptions<PricingDbContext> options): base(options) {}

        public DbSet<UserInput> UserInputs { get; set; }
        public DbSet<DbInput> DbInputs { get; set; }
    }
}

