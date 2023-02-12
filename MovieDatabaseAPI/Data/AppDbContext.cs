using System;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data.Entity;
using MovieDatabaseAPI.ErrorSaver;
using System.Data.SqlTypes;

namespace MovieDatabaseAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(sc => new { sc.MovieId, sc.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(sc => sc.Genre)
                .WithMany(c => c.MovieGenres)
                .HasForeignKey(sc => sc.GenreId);
        }
    }
}

