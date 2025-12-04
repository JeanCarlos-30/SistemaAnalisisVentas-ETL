using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Api;

namespace AnalisisVentas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET api/Clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.ObtenerClientesAsync();
            return Ok(clientes);
        }

        // GET api/Clientes/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);

            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado." });

            return Ok(cliente);
        }

        // POST api/Clientes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO cliente)
        {
            var resultado = await _clienteService.CrearClienteAsync(cliente);
            return Ok(new { mensaje = resultado });
        }

        // PUT api/Clientes
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClienteDTO cliente)
        {
            var resultado = await _clienteService.ActualizarClienteAsync(cliente);
            return Ok(new { mensaje = resultado });
        }

        // DELETE api/Clientes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _clienteService.EliminarClienteAsync(id);
            return Ok(new { mensaje = resultado });
        }
    }
}
