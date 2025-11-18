using SistemaAnalisisVentas.Application.DTOs.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Api
{
    public interface IVentaService
    {
        Task<List<VentaDTO>> ObtenerVentasAsync();
        Task<VentaDTO?> ObtenerVentaPorIdAsync(int id);
    }
}
