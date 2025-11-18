using SistemaAnalisisVentas.Application.DTOs.Api;

namespace AnalisisVentas.Api.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ObtenerClientesAsync();
        Task<ClienteDTO?> ObtenerClientePorIdAsync(int id);
    }
}
