using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using System.Data;

namespace SistemaAnalisisVentas.Infrastructure.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly string _connectionString;

        public VentaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
        }

        public async Task<IEnumerable<VentaDTO>> ObtenerVentasAsync()
        {
            var ventas = new List<VentaDTO>();

            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerVentas", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                ventas.Add(new VentaDTO
                {
                    OrderID = dr.GetInt32(0),
                    CustomerID = dr.GetString(1),
                    OrderDate = dr.IsDBNull(2) ? null : dr.GetDateTime(2),
                    ShipCountry = dr.IsDBNull(3) ? string.Empty : dr.GetString(3),
                    FuenteID = dr.IsDBNull(4) ? null : dr.GetInt32(4)
                });
            }

            return ventas;
        }

        public async Task<VentaDTO?> ObtenerVentaPorIdAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerVentaPorId", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderID", id);

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            if (await dr.ReadAsync())
            {
                return new VentaDTO
                {
                    OrderID = dr.GetInt32(0),
                    CustomerID = dr.GetString(1),
                    OrderDate = dr.IsDBNull(2) ? null : dr.GetDateTime(2),
                    ShipCountry = dr.IsDBNull(3) ? string.Empty : dr.GetString(3),
                    FuenteID = dr.IsDBNull(4) ? null : dr.GetInt32(4)
                };
            }

            return null;
        }

        public async Task<string> InsertarVentaAsync(VentaDTO v)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarVenta", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderID", v.OrderID);
            cmd.Parameters.AddWithValue("@CustomerID", v.CustomerID);
            cmd.Parameters.AddWithValue("@OrderDate", (object?)v.OrderDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShipCountry", v.ShipCountry);
            cmd.Parameters.AddWithValue("@FuenteID", v.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Venta creada correctamente.";
        }

        public async Task<string> ActualizarVentaAsync(VentaDTO v)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarVenta", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderID", v.OrderID);
            cmd.Parameters.AddWithValue("@CustomerID", v.CustomerID);
            cmd.Parameters.AddWithValue("@OrderDate", (object?)v.OrderDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShipCountry", v.ShipCountry);
            cmd.Parameters.AddWithValue("@FuenteID", v.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Venta actualizada correctamente.";
        }

        public async Task<string> EliminarVentaAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarVenta", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderID", id);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Venta eliminada correctamente.";
        }
    }
}
