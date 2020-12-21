using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaintNicholas.Data
{
    public class ChildrenConfiguation : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);

            builder.Property(e => e.StreetAddress).IsRequired().HasMaxLength(64);

            builder.Property(e => e.PostalCode).IsRequired().HasMaxLength(16);

            builder.Property(e => e.City).IsRequired().HasMaxLength(32);

            builder.Property(e => e.Country).IsRequired().HasMaxLength(32);
        }
    }
}
