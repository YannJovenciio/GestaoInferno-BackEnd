using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.OutBoxEvent;

public class OutBoxEventConfiguration : IEntityTypeConfiguration<Entity.OutBoxEvent>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entity.OutBoxEvent> builder
    )
    {
        builder.HasKey(x => x.OutBoxEventId);
        builder.Property(x => x.Type).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.ProcessedAt).IsRequired(false);
        builder.Property(x => x.Attempts).IsRequired();
        builder.Property(x => x.Error);
    }
}
