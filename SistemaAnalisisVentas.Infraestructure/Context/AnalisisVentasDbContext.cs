using Microsoft.EntityFrameworkCore;
using SistemaAnalisisVentas.Domain.Entities.API;
using SistemaAnalisisVentas.Domain.Entities.DB;

namespace SistemaAnalisisVentas.Infrastructure.Context
{
    public class AnalisisVentasDbContext : DbContext
    {
        public AnalisisVentasDbContext(DbContextOptions<AnalisisVentasDbContext> options)
            : base(options)
        {
        }

        // =======================================
        //  TABLAS MAPEADAS A TUS ENTIDADES ACTUALES
        // =======================================

        public DbSet<ClienteApi> Clientes { get; set; }
        public DbSet<ProductoApi> Productos { get; set; }
        public DbSet<VentaDb> Ventas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =======================================
            // CLIENTE API → TABLA: Clientes
            // =======================================
            modelBuilder.Entity<ClienteApi>(entity =>
            {
                entity.ToTable("Clientes");

                // PK REAL: CustomerID
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("CustomerID");

                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.City).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(50);
            });

            // =======================================
            // PRODUCTO API → TABLA: Productos
            // =======================================
            modelBuilder.Entity<ProductoApi>(entity =>
            {
                entity.ToTable("Productos");

                // PK REAL: ProductID
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ProductID");

                entity.Property(e => e.ProductName).HasMaxLength(100);
                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Stock).HasColumnName("Stock");
            });

            // =======================================
            // VENTA DB → TABLA: Orders
            // =======================================
            modelBuilder.Entity<VentaDb>(entity =>
            {
                entity.ToTable("Orders");

                // PK REAL: OrderID
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("OrderID");

                entity.Property(e => e.ClienteId).HasColumnName("CustomerID");

                // Mapeo directo: la tabla real NO tiene columnas de producto
                entity.Ignore(e => e.ProductoId);
                entity.Ignore(e => e.Cantidad);
                entity.Ignore(e => e.PrecioUnitario);
                entity.Ignore(e => e.Descuento);

                entity.Property(e => e.FechaVenta)
                      .HasColumnName("OrderDate");
            });
        }
    }
}
