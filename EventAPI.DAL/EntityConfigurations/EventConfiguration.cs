using EventAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventAPI.DAL.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.Name).HasMaxLength(80);
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Plan).HasMaxLength(400);
            builder.Property(e => e.Organizer).HasMaxLength(50);
            builder.Property(e => e.Speaker).HasMaxLength(50);
            builder.Property(e => e.EventPlace).HasMaxLength(100);
        }
    }
}
