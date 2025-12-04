using SistemaAnalisisVentas.Application.DTOs.Api;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDTO>> ObtenerClientesAsync();
        Task<ClienteDTO?> ObtenerClientePorIdAsync(int id);
        Task<string> InsertarClienteAsync(ClienteDTO cliente);
        Task<string> ActualizarClienteAsync(ClienteDTO cliente);
        Task<string> EliminarClienteAsync(int id);
    }
}
