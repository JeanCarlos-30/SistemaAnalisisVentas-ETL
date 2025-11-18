using Microsoft.EntityFrameworkCore;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Domain.Entities.DB;
using SistemaAnalisisVentas.Infrastructure.Context;

public class VentaRepository : IVentaRepository
{
    private readonly AnalisisVentasDbContext _context;

    public VentaRepository(AnalisisVentasDbContext context)
    {
        _context = context;
    }

    public Task<List<VentaDb>> ObtenerVentasAsync()
        => _context.Ventas.ToListAsync();

    public Task<VentaDb?> ObtenerVentaPorIdAsync(int id)
        => _context.Ventas.FirstOrDefaultAsync(x => x.Id == id);
}
