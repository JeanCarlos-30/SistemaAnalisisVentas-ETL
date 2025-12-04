using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repo;

        public VentaService(IVentaRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<VentaDTO>> ObtenerVentasAsync() =>
            _repo.ObtenerVentasAsync();

        public Task<VentaDTO?> ObtenerVentaPorIdAsync(int id) =>
            _repo.ObtenerVentaPorIdAsync(id);

        public Task<string> CrearVentaAsync(VentaDTO venta) =>
            _repo.InsertarVentaAsync(venta);

        public Task<string> ActualizarVentaAsync(VentaDTO venta) =>
            _repo.ActualizarVentaAsync(venta);

        public Task<string> EliminarVentaAsync(int id) =>
            _repo.EliminarVentaAsync(id);
    }
}
