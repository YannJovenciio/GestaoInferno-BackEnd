using Inferno.src.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inferno.src.Configuration;

public class HellTaskConfiguration : IEntityTypeConfiguration<HellTask>
{
    public void Configure(EntityTypeBuilder<HellTask> builder)
    {
        builder.HasKey(h => h.HellTaskId);
        builder.Property(h => h.Title).HasMaxLength(100);
        builder.Property(h => h.Description).HasMaxLength(100);
        builder.Property(h => h.CreatedAt);
        builder.Property(h => h.DeadLine);
        builder.Property(h => h.Status);
        builder.Property(h => h.Progress);
        builder.Property(h => h.UpdatedAt).IsRequired(false);
        builder.Property(h => h.CompletedAt).IsRequired(false);
        builder.Property(h => h.Priority);
        builder.HasOne(h => h.Demon).WithMany(d => d.HellTasks).HasForeignKey(h => h.DemonId);
    }
}
