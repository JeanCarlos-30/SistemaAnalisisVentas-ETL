using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.DTOs;
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

        // -----------------------------------------------------------
        // GET api/Productos
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.ObtenerProductosAsync();
            return Ok(productos);
        }

        // -----------------------------------------------------------
        // GET api/Productos/5
        // -----------------------------------------------------------
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);

            if (producto == null)
                return NotFound(new { mensaje = $"Producto con ID {id} no encontrado." });

            return Ok(producto);
        }

        // -----------------------------------------------------------
        // POST api/Productos
        // -----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDTO producto)
        {
            if (producto == null)
                return BadRequest(new { mensaje = "El producto no puede ser nulo." });

            var resultado = await _productoService.CrearProductoAsync(producto);
            return Ok(new { mensaje = resultado });
        }

        // -----------------------------------------------------------
        // PUT api/Productos
        // -----------------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductoDTO producto)
        {
            if (producto == null)
                return BadRequest(new { mensaje = "El producto no puede ser nulo." });

            var resultado = await _productoService.ActualizarProductoAsync(producto);
            return Ok(new { mensaje = resultado });
        }

        // -----------------------------------------------------------
        // DELETE api/Productos/5
        // -----------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _productoService.EliminarProductoAsync(id);
            return Ok(new { mensaje = resultado });
        }
    }
}
