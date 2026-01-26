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
    public DbSet<OutBoxEvent> OutBoxEvent { get; set; }
    public string DbPath { get; }

    public HellDbContext(DbContextOptions<HellDbContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Hell.db");
    }

    public HellDbContext()
    {
        DbPath = GetDbPath();
    }

    private static string GetDbPath()
    {
        var projectPath = AppContext.BaseDirectory;
        if (projectPath.Contains("bin"))
        {
            projectPath =
                System.IO.Path.GetDirectoryName(
                    System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(projectPath))
                )
                ?? projectPath;
        }
        return System.IO.Path.Join(projectPath, "Hell.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source ={DbPath};Foreign Keys=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HellDbContext).Assembly);
    }
}
