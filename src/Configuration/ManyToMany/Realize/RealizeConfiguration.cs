using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using entity = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Configuration.ManyToMany.Realize
{
    public class RealizeConfiguration : IEntityTypeConfiguration<entity.Realize>
    {
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<entity.Realize> builder
        )
        {
            builder.HasKey(r => new { r.IdSin, r.IdSoul });
            builder
                .HasOne(r => r.Soul)
                .WithMany(s => s.Realizes)
                .HasForeignKey(r => r.IdSoul)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(r => r.Sin)
                .WithMany(s => s.Realizes)
                .HasForeignKey(r => r.IdSin)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
