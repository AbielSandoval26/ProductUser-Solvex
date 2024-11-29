using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }

        public DbSet<Users> Users { get; set; }

        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Descripcion)
                    .HasMaxLength(500);

                entity.Property(p => p.ImagenUrl)
                    .HasMaxLength(int.MaxValue);

                entity.Property(p => p.FechaCreacion)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.FechaModificacion)
                    .IsRequired(false);
            });

            modelBuilder.Entity<ProductVariation>(entity =>
            {
                entity.ToTable("ProductVariations");
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(v => v.Precio)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(v => v.FechaCreacion)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(v => v.FechaModificacion)
                    .IsRequired(false);

                entity.HasOne<Product>()
                    .WithMany(p => p.Variaciones)
                    .HasForeignKey(v => v.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(v => new { v.ProductoId, v.Color })
                    .IsUnique()
                    .HasDatabaseName("UQ_Producto_Color");
            });

            modelBuilder.Entity<ProductWithVariationsView>(entity =>
            {
                entity.ToView("vw_ProductosConVariaciones");
                entity.HasNoKey(); 

                entity.Property(v => v.ProductoId).HasColumnName("ProductoId");
                entity.Property(v => v.Producto).HasColumnName("Producto");
                entity.Property(v => v.Color).HasColumnName("Color");
                entity.Property(v => v.Precio).HasColumnName("Precio");
                entity.Property(v => v.Descripcion).HasColumnName("Descripcion");
                entity.Property(v => v.ImagenUrl).HasColumnName("ImagenUrl");
                entity.Property(v => v.FechaCreacion).HasColumnName("FechaCreacion");
                entity.Property(v => v.FechaModificacion).HasColumnName("FechaModificacion");
            });

        }
    }
}
