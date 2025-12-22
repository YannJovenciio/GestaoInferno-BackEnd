using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Hell
{
    public class HellConfiguration : IEntityTypeConfiguration<entity.Hell>
    {
        public void Configure(EntityTypeBuilder<entity.Hell> builder)
        {
            builder.HasKey(h => h.IdHell);
            builder.Property(h => h.Descricao);
            builder.Property(h => h.Nivel);
            builder.Property(h => h.Nome);
        }
    }
}
