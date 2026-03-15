using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Inferno.src.Configuration.ManyToMany
{
    public class DemonRoleConfiguration : IEntityTypeConfiguration<DemonRole>
    {
        public void Configure(EntityTypeBuilder<DemonRole> builder)
        {
            builder.HasKey(dr => new { dr.DemonId, dr.RoleId });

            builder
                .HasOne(dr => dr.Demon)
                .WithMany(d => d.DemonRoles)
                .HasForeignKey(dr => dr.DemonId);

            builder
                .HasOne(dr => dr.Role)
                .WithMany(r => r.DemonRoles)
                .HasForeignKey(dr => dr.RoleId);
            // Role seeding moved to RoleConfiguration to avoid seeding a different entity type here.
        }
    }
}
