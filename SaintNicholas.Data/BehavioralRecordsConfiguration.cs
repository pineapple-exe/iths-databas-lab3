using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaintNicholas.Data.Entities;

namespace SaintNicholas.Data
{
    public class BehavioralRecordsConfiguration : IEntityTypeConfiguration<BehavioralRecord>
    {
        public void Configure(EntityTypeBuilder<BehavioralRecord> builder)
        {
            builder.HasKey(e => new { e.ChildID, e.Year });

            builder.Property(e => e.ChildID); //foreign key osv???

            builder.Property(e => e.Year).IsRequired();

            builder.Property(e => e.Naughty);
        }
    }
}
