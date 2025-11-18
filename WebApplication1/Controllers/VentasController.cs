using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.Interfaces.Api;

namespace AnalisisVentas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _ventaService.ObtenerVentasAsync();
            return Ok(ventas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _ventaService.ObtenerVentaPorIdAsync(id);

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }
    }
}
