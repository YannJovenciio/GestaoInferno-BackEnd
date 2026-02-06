using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Hell
{
    public class HellConfiguration : IEntityTypeConfiguration<Entity.Hell>
    {
        public void Configure(EntityTypeBuilder<Entity.Hell> builder)
        {
            builder.HasKey(h => h.IdHell);
            builder.Property(h => h.Descricao);
            builder.Property(h => h.Nivel);
            builder.Property(h => h.HellName);
        }
    }
}
