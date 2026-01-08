using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Configuration.Persecution
{
    public class PersecutionConfiguration : IEntityTypeConfiguration<Entity.Persecution>
    {
        public void Configure(EntityTypeBuilder<Entity.Persecution> builder)
        {
            builder.HasKey(p => new { p.IdDemon, p.IdSoul });

            builder
                .HasOne(p => p.Demon)
                .WithMany(d => d.Persecutions)
                .HasForeignKey(p => p.IdDemon)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Soul)
                .WithMany(s => s.Persecutions)
                .HasForeignKey(p => p.IdSoul)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
