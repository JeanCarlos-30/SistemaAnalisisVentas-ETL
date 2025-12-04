using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.DTOs;
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
                return NotFound(new { mensaje = $"Venta con ID {id} no encontrada." });

            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VentaDTO venta)
        {
            var resultado = await _ventaService.CrearVentaAsync(venta);
            return Ok(new { mensaje = resultado });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VentaDTO venta)
        {
            var resultado = await _ventaService.ActualizarVentaAsync(venta);
            return Ok(new { mensaje = resultado });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _ventaService.EliminarVentaAsync(id);
            return Ok(new { mensaje = resultado });
        }
    }
}
