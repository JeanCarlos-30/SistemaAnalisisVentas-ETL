using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IVentaRepository
    {
        Task<IEnumerable<VentaDTO>> ObtenerVentasAsync();
        Task<VentaDTO?> ObtenerVentaPorIdAsync(int id);
        Task<string> InsertarVentaAsync(VentaDTO venta);
        Task<string> ActualizarVentaAsync(VentaDTO venta);
        Task<string> EliminarVentaAsync(int id);
    }
}
