using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Configuration.Category
{
    public class CategoryConfiguration : IEntityTypeConfiguration<entity.Category>
    {
        public void Configure(EntityTypeBuilder<entity.Category> builder)
        {
            builder.ToTable(nameof(entity.Category));
            builder.HasKey(c => c.IdCategory);
            builder.Property(c => c.IdCategory).HasColumnName(nameof(entity.Category.IdCategory));
            builder
                .Property(c => c.CategoryName)
                .IsRequired()
                .HasColumnName(nameof(entity.Category.CategoryName));
            builder
                .HasMany(c => c.Demons)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);
        }
    }
}
