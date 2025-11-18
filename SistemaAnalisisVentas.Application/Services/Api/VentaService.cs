using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Application.Mappers.Db;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<List<VentaDTO>> ObtenerVentasAsync()
        {
            var ventas = await _ventaRepository.ObtenerVentasAsync();
            return ventas.Select(VentaDbMapper.ToDto).ToList();
        }

        public async Task<VentaDTO?> ObtenerVentaPorIdAsync(int id)
        {
            var venta = await _ventaRepository.ObtenerVentaPorIdAsync(id);

            return venta == null ? null : VentaDbMapper.ToDto(venta);
        }
    }
}
