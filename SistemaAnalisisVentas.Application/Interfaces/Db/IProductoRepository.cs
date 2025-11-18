using SistemaAnalisisVentas.Domain.Entities.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IProductoRepository
    {
        Task<List<ProductoApi>> ObtenerProductosAsync();
        Task<ProductoApi?> ObtenerProductoPorIdAsync(int id);
    }
}
