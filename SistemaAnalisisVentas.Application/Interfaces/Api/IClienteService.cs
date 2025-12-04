using SistemaAnalisisVentas.Application.DTOs.Api;

namespace SistemaAnalisisVentas.Application.Interfaces.Api
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> ObtenerClientesAsync();
        Task<ClienteDTO?> ObtenerClientePorIdAsync(int id);
        Task<string> CrearClienteAsync(ClienteDTO cliente);
        Task<string> ActualizarClienteAsync(ClienteDTO cliente);
        Task<string> EliminarClienteAsync(int id);
    }
}
