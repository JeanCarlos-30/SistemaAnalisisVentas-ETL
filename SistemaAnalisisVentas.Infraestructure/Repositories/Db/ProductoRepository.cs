using Microsoft.EntityFrameworkCore;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Domain.Entities.API;
using SistemaAnalisisVentas.Infrastructure.Context;

public class ProductoRepository : IProductoRepository
{
    private readonly AnalisisVentasDbContext _context;

    public ProductoRepository(AnalisisVentasDbContext context)
    {
        _context = context;
    }

    public Task<List<ProductoApi>> ObtenerProductosAsync()
        => _context.Productos.ToListAsync();

    public Task<ProductoApi?> ObtenerProductoPorIdAsync(int id)
        => _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
}
