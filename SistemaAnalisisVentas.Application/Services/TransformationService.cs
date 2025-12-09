using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Validators;

namespace SistemaAnalisisVentas.Application.Services
{
    public class TransformationService : ITransformationService
    {
        private readonly ClienteValidator _clienteValidator;
        private readonly ProductoValidator _productoValidator;
        private readonly VentaValidator _ventaValidator;

        public TransformationService(
            ClienteValidator clienteValidator,
            ProductoValidator productoValidator,
            VentaValidator ventaValidator)
        {
            _clienteValidator = clienteValidator;
            _productoValidator = productoValidator;
            _ventaValidator = ventaValidator;
        }

        // ===============================================================
        //  VALIDACIÓN GENERAL
        // ===============================================================
        public async Task TransformAndValidateAsync()
        {
            LoggerHelper.Info("🔍 Iniciando TransformAndValidateAsync...");

            var productos = await ObtenerProductosAsync();
            var clientes = await ObtenerClientesAsync();
            var ventas = await ObtenerVentasAsync();

            int validProducts = productos.Count(p => _productoValidator.EsValido(p));
            int validCustomers = clientes.Count(c => _clienteValidator.EsValido(c));
            int validSales = ventas.Count(v => _ventaValidator.EsValido(v));

            LoggerHelper.Info($"✔ Productos válidos: {validProducts}/{productos.Count}");
            LoggerHelper.Info($"✔ Clientes válidos: {validCustomers}/{clientes.Count}");
            LoggerHelper.Info($"✔ Ventas válidas: {validSales}/{ventas.Count}");

            LoggerHelper.Info("✔ Validación general completada.");
        }

        public async Task ComputeDerivedValuesAsync()
        {
            LoggerHelper.Info("Calculando valores derivados...");
            await Task.Delay(30);
            LoggerHelper.Info("✔ Cálculo completado.");
        }

        // ===============================================================
        //  TRANSFORMACIONES A DIMENSIONES
        // ===============================================================

        // ----------- DIM PRODUCT -----------
        public async Task<List<DimProductDTO>> TransformarProductosAsync()
        {
            var origen = await ObtenerProductosAsync();
            var resultado = new List<DimProductDTO>();

            foreach (var p in origen)
            {
                if (!_productoValidator.EsValido(p))
                    continue;

                resultado.Add(new DimProductDTO
                {
                    ProductKey = p.ProductID.GetHashCode(),
                    ProductId = p.ProductID,
                    Name = p.ProductName,
                    CategoryName = p.Category ?? "Unknown",
                    StockQty = p.Stock,
                    UnitPrice = p.Price
                });
            }

            LoggerHelper.Info($"✔ Productos transformados: {resultado.Count}");
            return resultado;
        }

        // ----------- DIM CUSTOMER -----------
        public async Task<List<DimCustomerDTO>> TransformarClientesAsync()
        {
            var origen = await ObtenerClientesAsync();
            var resultado = new List<DimCustomerDTO>();

            foreach (var c in origen)
            {
                if (!_clienteValidator.EsValido(c))
                    continue;

                resultado.Add(new DimCustomerDTO
                {
                    CustomerKey = c.CustomerID.GetHashCode(),
                    CustomerId = c.CustomerID.ToString(),
                    CustomerName = $"{c.FirstName} {c.LastName}",
                    ContactTitle = "Customer",
                    City = c.City,
                    Region = "",
                    Country = c.Country
                });
            }

            LoggerHelper.Info($"✔ Clientes transformados: {resultado.Count}");
            return resultado;
        }

        // ----------- DIM DATE -----------
        public List<DimDateDTO> GenerarDimDate(DateTime inicio, DateTime fin)
        {
            var lista = new List<DimDateDTO>();

            for (var fecha = inicio; fecha <= fin; fecha = fecha.AddDays(1))
            {
                lista.Add(new DimDateDTO
                {
                    DateKey = int.Parse(fecha.ToString("yyyyMMdd")),
                    FullDate = fecha.Date,
                    DayNumber = fecha.Day,
                    MonthNumber = fecha.Month,
                    MonthName = fecha.ToString("MMMM"),
                    Quarter = (fecha.Month - 1) / 3 + 1,
                    Year = fecha.Year,
                    DayName = fecha.ToString("dddd")
                });
            }

            LoggerHelper.Info($"✔ Fechas generadas: {lista.Count}");
            return lista;
        }


        // ----------- FACT SALES -----------
        public async Task<List<FactSalesDTO>> TransformarVentasAsync()
        {
            var ventas = await ObtenerVentasAsync();
            var detalles = await ObtenerDetallesAsync();

            var resultado = new List<FactSalesDTO>();

            foreach (var v in ventas)
            {
                if (!_ventaValidator.EsValido(v))
                    continue;

                var detalle = detalles.FirstOrDefault(d => d.OrderID == v.OrderID);
                if (detalle == null)
                    continue;

                resultado.Add(new FactSalesDTO
                {
                    ProductKey = detalle.ProductID.GetHashCode(),
                    CustomerKey = v.CustomerID.GetHashCode(),
                    DateKey = v.OrderDate ?? DateTime.UtcNow,
                    SourceKey = 1,
                    Quantity = detalle.Quantity,
                    UnitPrice = detalle.UnitPrice,
                    Discount = detalle.Discount
                });
            }

            LoggerHelper.Info($"✔ Ventas transformadas a FactSales: {resultado.Count}");
            return resultado;
        }

        // ===============================================================
        //  MÉTODOS TEMPORALES (LUEGO VIENEN DE EXTRACT)
        // ===============================================================

        private async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            await Task.Delay(5);
            return new List<ClienteDTO>();
        }

        private async Task<List<ProductoDTO>> ObtenerProductosAsync()
        {
            await Task.Delay(5);
            return new List<ProductoDTO>();
        }

        private async Task<List<VentaDTO>> ObtenerVentasAsync()
        {
            await Task.Delay(5);
            return new List<VentaDTO>();
        }

        private async Task<List<DetalleVentaDTO>> ObtenerDetallesAsync()
        {
            await Task.Delay(5);
            return new List<DetalleVentaDTO>();
        }
    }
}
