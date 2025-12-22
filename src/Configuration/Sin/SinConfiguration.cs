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
            builder.Property(s => s.SinName);
            builder.Property(s => s.SinSeverity);
            builder.HasOne(s => s.Soul).WithMany(so => so.Sins).HasForeignKey(s => s.IdSoul);
        }
    }
}
