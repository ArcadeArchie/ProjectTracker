using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;

namespace Desktop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrackingEntry> TrackingEntries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appDb.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TrackingEntry>()
            .HasKey(x => x.TimeStamp);
        }
    }
}