using Microsoft.EntityFrameworkCore;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Domain.Entities.API;
using SistemaAnalisisVentas.Infrastructure.Context;

namespace SistemaAnalisisVentas.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AnalisisVentasDbContext _context;

        public ClienteRepository(AnalisisVentasDbContext context)
        {
            _context = context;
        }

        public Task<List<ClienteApi>> ObtenerClientesAsync()
            => _context.Clientes.ToListAsync();

        public Task<ClienteApi?> ObtenerClientePorIdAsync(int id)
            => _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
    }
}
