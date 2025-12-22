using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Cavern
{
    public class CavernConfiguration : IEntityTypeConfiguration<entity.Cavern>
    {
        public void Configure(EntityTypeBuilder<entity.Cavern> builder)
        {
            builder.HasKey(c => c.IdCavern);
            builder.Property(c => c.Location).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Capacity).IsRequired();
        }
    }
}
