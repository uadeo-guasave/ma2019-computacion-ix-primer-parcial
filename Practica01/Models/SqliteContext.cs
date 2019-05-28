using Microsoft.EntityFrameworkCore;

namespace Practica01.Models
{
  public class SqliteContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data source=Db/CensoAgua.db");

      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // TODO: codigo para representar la tabla de claves de localización
      modelBuilder.Entity<ClaveDeLocalizacion>(clave =>
      {
        clave.Property(c => c.Id).HasColumnName("id");
        clave.Property(c => c.Subsistema).HasColumnName("subsistema");
        clave.Property(c => c.Sector).HasColumnName("sector");
        clave.Property(c => c.Manzana).HasColumnName("manzana");
        clave.Property(c => c.Lote).HasColumnName("lote");
        clave.Property(c => c.Nivel1).HasColumnName("nivel1");
        clave.Property(c => c.Nivel2).HasColumnName("nivel2");
        clave.Property(c => c.Fraccion).HasColumnName("fraccion");
        clave.Property(c => c.Toma).HasColumnName("toma");
        clave.Property(c => c.Original).HasColumnName("original");
        clave.ToTable("claves_de_localizacion");
      });

      base.OnModelCreating(modelBuilder);
    }

    public DbSet<ClaveDeLocalizacion> ClavesDeLocalizacion { get; set; }
  }
}