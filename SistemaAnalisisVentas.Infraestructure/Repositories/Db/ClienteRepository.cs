using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using System.Data;

namespace SistemaAnalisisVentas.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerClientesAsync()
        {
            var clientes = new List<ClienteDTO>();

            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerClientes", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                clientes.Add(new ClienteDTO
                {
                    CustomerID = dr.GetInt32(0),
                    FirstName = dr.GetString(1),
                    LastName = dr.GetString(2),
                    Email = dr.IsDBNull(3) ? null : dr.GetString(3),
                    Phone = dr.IsDBNull(4) ? null : dr.GetString(4),
                    City = dr.IsDBNull(5) ? null : dr.GetString(5),
                    Country = dr.IsDBNull(6) ? null : dr.GetString(6),
                    FuenteID = dr.IsDBNull(7) ? null : dr.GetInt32(7)
                });
            }

            return clientes;
        }

        public async Task<ClienteDTO?> ObtenerClientePorIdAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerClientePorId", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", id);

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            if (await dr.ReadAsync())
            {
                return new ClienteDTO
                {
                    CustomerID = dr.GetInt32(0),
                    FirstName = dr.GetString(1),
                    LastName = dr.GetString(2),
                    Email = dr.IsDBNull(3) ? null : dr.GetString(3),
                    Phone = dr.IsDBNull(4) ? null : dr.GetString(4),
                    City = dr.IsDBNull(5) ? null : dr.GetString(5),
                    Country = dr.IsDBNull(6) ? null : dr.GetString(6),
                    FuenteID = dr.IsDBNull(7) ? null : dr.GetInt32(7)
                };
            }

            return null;
        }

        public async Task<string> InsertarClienteAsync(ClienteDTO c)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarCliente", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@Email", c.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", c.Phone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@City", c.City ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", c.Country ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FuenteID", c.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Cliente insertado correctamente.";
        }

        public async Task<string> ActualizarClienteAsync(ClienteDTO c)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarCliente", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@Email", c.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", c.Phone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@City", c.City ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", c.Country ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FuenteID", c.FuenteID ?? (object)DBNull.Value);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Cliente actualizado correctamente.";
        }

        public async Task<string> EliminarClienteAsync(int id)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarCliente", cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", id);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return "Cliente eliminado correctamente.";
        }
    }
}
