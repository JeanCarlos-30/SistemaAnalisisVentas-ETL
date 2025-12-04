using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Application.DTOs.Api;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ClienteDTO>> ObtenerClientesAsync() =>
            _repo.ObtenerClientesAsync();

        public Task<ClienteDTO?> ObtenerClientePorIdAsync(int id) =>
            _repo.ObtenerClientePorIdAsync(id);

        public Task<string> CrearClienteAsync(ClienteDTO cliente) =>
            _repo.InsertarClienteAsync(cliente);

        public Task<string> ActualizarClienteAsync(ClienteDTO cliente) =>
            _repo.ActualizarClienteAsync(cliente);

        public Task<string> EliminarClienteAsync(int id) =>
            _repo.EliminarClienteAsync(id);
    }
}
