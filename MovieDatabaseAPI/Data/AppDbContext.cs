using System;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data.Entity;

namespace MovieDatabaseAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } 
    }
}

