using AnalisisVentas.Api.Services;
using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.Interfaces.Api;

namespace AnalisisVentas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.ObtenerProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }
    }
}
