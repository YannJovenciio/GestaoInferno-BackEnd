using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;
using EntityM = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories;

public class HellDbContext : DbContext
{
    public DbSet<Entity.Hell> Hell { get; set; }
    public DbSet<Entity.Cavern> Caverns { get; set; }
    public DbSet<Entity.Demon> Demons { get; set; }
    public DbSet<Entity.Sin> Sins { get; set; }
    public DbSet<Entity.Soul> Souls { get; set; }
    public DbSet<Entity.Category> Categories { get; set; }
    public DbSet<EntityM.Persecution> Persecution { get; set; }
    public DbSet<Entity.OutBoxEvent> OutBoxEvent { get; set; }
    public DbSet<Entity.Image> Image { get; set; }
    public DbSet<Entity.HellTask> HellTasks { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<SigninKey> SigninKeys { get; set; }
    public DbSet<DemonRole> DemonRoles { get; set; }
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
