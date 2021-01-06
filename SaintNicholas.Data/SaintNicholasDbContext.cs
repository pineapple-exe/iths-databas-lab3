using Microsoft.EntityFrameworkCore;
using SaintNicholas.Data.Entities;

namespace SaintNicholas.Data
{
    public class SaintNicholasDbContext : DbContext
    {
        public DbSet<Child> Children { get; set; }
        public DbSet<ChristmasPresent> ChristmasPresents { get; set; }

        public DbSet<BehavioralRecord> BehavioralRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=SaintNicholasWorkshop;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChildrenConfiguation());
            modelBuilder.ApplyConfiguration(new ChristmasPresentsConfiguration());
            modelBuilder.ApplyConfiguration(new BehavioralRecordsConfiguration());
        }
    }
}
