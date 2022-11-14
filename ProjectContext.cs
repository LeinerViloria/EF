using Microsoft.EntityFrameworkCore;
using Models;

namespace Concesionario;

public class ProjectContext : DbContext
{
    public DbSet<Auto> Auto {get; set;}

    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Auto>(Auto => {
            // Auto.ToTable("Auto");
            //Auto.HasKey(x => x.Rowid);

            // Auto.Property( p => p.Placa).IsRequired().HasMaxLength(6);
            // Auto.Property( p => p.Marca).IsRequired().HasMaxLength(15);
            // Auto.Property( p => p.Modelo).IsRequired();
            // Auto.Property( p => p.Color).IsRequired().HasMaxLength(10);
        });
    }
}