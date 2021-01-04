using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaintNicholas.Data
{
    public class ChristmasPresentsConfiguration : IEntityTypeConfiguration<ChristmasPresent>
    {
        public void Configure(EntityTypeBuilder<ChristmasPresent> builder)
        {
            builder.Property(e => e.Contents).IsRequired().HasMaxLength(64);

            builder.Property(e => e.ForGender).IsRequired().HasMaxLength(4);
        }
    }
}
