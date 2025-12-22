using Inferno.src.Configuration.Category;
using Inferno.src.Configuration.Cavern;
using Inferno.src.Configuration.Demon;
using Inferno.src.Configuration.Hell;
using Inferno.src.Configuration.ManyToMany.Realize;
using Inferno.src.Configuration.Persecution;
using Inferno.src.Configuration.Sin;
using Inferno.src.Configuration.Soul;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace Inferno.src.Adapters.Outbound.Persistence;

/// <summary>
/// Contexto do banco de dados - Adaptador de saída (Outbound)
/// </summary>
public class HellDbContext : DbContext
{
    public DbSet<Hell> Hell { get; set; }
    public DbSet<Cavern> Caverns { get; set; }
    public DbSet<Demon> Demons { get; set; }
    public DbSet<Sin> Sins { get; set; }
    public DbSet<Soul> Souls { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Persecution> Persecution { get; set; }
    public string DbPath { get; }

    public HellDbContext(DbContextOptions<HellDbContext> options)
        : base(options)
    {
        DbPath = GetDbPath();
    }

    public HellDbContext()
    {
        DbPath = GetDbPath();
    }

    private static string GetDbPath()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        return System.IO.Path.Join(path, "Hell.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($"Data Source ={DbPath}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HellDbContext).Assembly);
    }
}
