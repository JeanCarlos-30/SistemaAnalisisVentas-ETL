using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Api
{
    public interface IVentaService
    {
        Task<IEnumerable<VentaDTO>> ObtenerVentasAsync();
        Task<VentaDTO?> ObtenerVentaPorIdAsync(int id);
        Task<string> CrearVentaAsync(VentaDTO venta);
        Task<string> ActualizarVentaAsync(VentaDTO venta);
        Task<string> EliminarVentaAsync(int id);
    }
}
