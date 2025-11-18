using SistemaAnalisisVentas.Domain.Entities.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IClienteRepository
    {
        Task<List<ClienteApi>> ObtenerClientesAsync();
        Task<ClienteApi?> ObtenerClientePorIdAsync(int id);
    }
}
