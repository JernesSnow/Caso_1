using AccesoADatos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace AccesoADatos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Servicio> Servicios => Set<Servicio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Servicio>().Property(p => p.Monto).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Servicio>().Property(p => p.IVA).HasColumnType("decimal(18,2)");
        }
    }
}