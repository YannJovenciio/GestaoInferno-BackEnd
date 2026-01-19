using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Sin
{
    public class SinConfiguration : IEntityTypeConfiguration<entity.Sin>
    {
        public void Configure(EntityTypeBuilder<entity.Sin> builder)
        {
            builder.HasKey(s => s.IdSin);
            builder.Property(s => s.SinName).IsRequired();
            builder.Property(s => s.SinSeverity).IsRequired();
            builder.Property(s => s.IdSoul).IsRequired(false);
            builder.HasOne(s => s.Soul).WithMany(so => so.Sins).HasForeignKey(s => s.IdSoul).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
