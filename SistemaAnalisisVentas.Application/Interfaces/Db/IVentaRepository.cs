using SistemaAnalisisVentas.Domain.Entities.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IVentaRepository
    {
        Task<List<VentaDb>> ObtenerVentasAsync();
        Task<VentaDb?> ObtenerVentaPorIdAsync(int id);
    }
}
