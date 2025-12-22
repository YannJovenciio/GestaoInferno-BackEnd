using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Demon
{
    public class DemonConfiguration : IEntityTypeConfiguration<entity.Demon>
    {
        public void Configure(EntityTypeBuilder<entity.Demon> builder)
        {
            builder.HasKey(d => d.IdDemon);
            builder.Property(d => d.DemonName).IsRequired();
            builder.Property(d => d.CreatedAt);
            builder.Property(d => d.UpdatedAt).IsRequired(false);

            builder.Property(d => d.CategoryId).IsRequired();

            builder
                .HasOne(d => d.Category)
                .WithMany(c => c.Demons)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
