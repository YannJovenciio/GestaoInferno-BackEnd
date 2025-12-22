using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Category
{
    public class CategoryConfiguration : IEntityTypeConfiguration<entity.Category>
    {
        public void Configure(EntityTypeBuilder<entity.Category> builder)
        {
            builder.HasKey(c => c.IdCategoria);
            builder.Property(c => c.NomeCategoria).IsRequired();
            builder
                .HasMany(c => c.Demons)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);
        }
    }
}
