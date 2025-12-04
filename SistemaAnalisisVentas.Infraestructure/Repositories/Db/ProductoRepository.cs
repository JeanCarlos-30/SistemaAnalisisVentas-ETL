using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using System.Data;

namespace SistemaAnalisisVentas.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Cadena de conexión 'DefaultConnection' no encontrada.");
        }

        public async Task<IEnumerable<ProductoDTO>> ObtenerProductosAsync()
        {
            var productos = new List<ProductoDTO>();

            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerProductos", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                productos.Add(new ProductoDTO
                {
                    ProductID = dr.GetInt32(0),
                    ProductName = dr.GetString(1),
                    Category = dr.IsDBNull(2) ? null : dr.GetString(2),
                    Price = dr.GetDecimal(3),
                    Stock = dr.GetInt32(4),
                    FuenteID = dr.IsDBNull(5) ? null : dr.GetInt32(5)
                });
            }

            return productos;
        }

        public async Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerProductoPorId", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductID", id);

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            if (await dr.ReadAsync())
            {
                return new ProductoDTO
                {
                    ProductID = dr.GetInt32(0),
                    ProductName = dr.GetString(1),
                    Category = dr.IsDBNull(2) ? null : dr.GetString(2),
                    Price = dr.GetDecimal(3),
                    Stock = dr.GetInt32(4),
                    FuenteID = dr.IsDBNull(5) ? null : dr.GetInt32(5)
                };
            }

            return null;
        }

        public async Task<string> InsertarProductoAsync(ProductoDTO p)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarProducto", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@Category", p.Category ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", p.Price);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@FuenteID", p.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Producto insertado correctamente.";
        }

        public async Task<string> ActualizarProductoAsync(ProductoDTO p)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarProducto", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@Category", p.Category ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", p.Price);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@FuenteID", p.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Producto actualizado correctamente.";
        }

        public async Task<string> EliminarProductoAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarProducto", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductID", id);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Producto eliminado correctamente.";
        }
    }
}
