using System;
using APIProject.Db.Entity;
using APIProject.Db;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Db
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FeedEntity> Feeds { get; set; }
        public DbSet<WebSiteEntity> Urls { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedTag>()
                .HasKey(sc => new { sc.FeedEntityId, sc.TagEntityId });

            modelBuilder.Entity<FeedTag>()
                .HasOne(sc => sc.FeedEntity)
                .WithMany(s => s.FeedTags)
                .HasForeignKey(sc => sc.FeedEntityId);

            modelBuilder.Entity<FeedTag>()
                .HasOne(sc => sc.TagEntity)
                .WithMany(c => c.FeedTags)
                .HasForeignKey(sc => sc.TagEntityId);

            modelBuilder.Entity<FeedEntity>()
                .HasOne(p => p.WebSiteEntity)
                .WithMany(b => b.Feeds)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

