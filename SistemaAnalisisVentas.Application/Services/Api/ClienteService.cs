using AnalisisVentas.Api.Services;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Application.Mappers.Api;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            var entities = await _repo.ObtenerClientesAsync();
            return entities.Select(ClienteApiMapper.ToDto).ToList();
        }

        public async Task<ClienteDTO?> ObtenerClientePorIdAsync(int id)
        {
            var entity = await _repo.ObtenerClientePorIdAsync(id);
            return entity == null ? null : ClienteApiMapper.ToDto(entity);
        }
    }
}
