using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;

namespace ProjectTracker.Desktop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TrackingEntry> TrackingEntries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appDb.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackingEntry>().HasOne(x => x.Project).WithMany(x => x.TrackingEntries);
            base.OnModelCreating(modelBuilder);
        }
    }
}