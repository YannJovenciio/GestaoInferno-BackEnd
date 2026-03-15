using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inferno.src.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);

            builder.Property(r => r.Description).IsRequired(false);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Admin Role",
                },
                new Role
                {
                    Id = 2,
                    Name = "Editor",
                    Description = "Editor Role",
                },
                new Role
                {
                    Id = 3,
                    Name = "Demon",
                    Description = "Demon Role",
                }
            );
        }
    }
}
