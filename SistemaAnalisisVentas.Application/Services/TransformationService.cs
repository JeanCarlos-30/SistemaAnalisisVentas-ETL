using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Validators;

namespace SistemaAnalisisVentas.Application.Services
{
    /// <summary>
    /// Servicio encargado de transformar, validar y preparar los datos extraídos
    /// para insertarlos en el Data Warehouse.
    /// </summary>
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
        //   MÉTODOS OBLIGATORIOS DE LA INTERFAZ
        // ===============================================================

        /// <summary>
        /// Realiza validaciones generales de datos antes del proceso ETL.
        /// </summary>
        public async Task TransformAndValidateAsync()
        {
            LoggerHelper.Info(" Iniciando TransformAndValidateAsync...");

            var productos = await ObtenerProductosAsync();
            var clientes = await ObtenerClientesAsync();
            var ventas = await ObtenerVentasAsync();

            int validProducts = productos.Count(p => _productoValidator.EsValido(p));
            int validCustomers = clientes.Count(c => _clienteValidator.EsValido(c));
            int validSales = ventas.Count(v => _ventaValidator.EsValido(v));

            LoggerHelper.Info($" Productos válidos: {validProducts}/{productos.Count}");
            LoggerHelper.Info($" Clientes válidos: {validCustomers}/{clientes.Count}");
            LoggerHelper.Info($" Ventas válidas: {validSales}/{ventas.Count}");

            LoggerHelper.Info(" Validación general completada.");
        }

        /// <summary>
        /// Calcula valores derivados o agregados necesarios para métricas del DWH.
        /// </summary>
        public async Task ComputeDerivedValuesAsync()
        {
            LoggerHelper.Info("Calculando valores derivados...");

            await Task.Delay(50); // Simulación

            LoggerHelper.Info("Valores derivados calculados.");
        }

        // ===============================================================
        //   TRANSFORMACIÓN A DIMENSIONES
        // ===============================================================

        /// <summary>
        /// Transforma los productos extraídos en DimProductDTO.
        /// </summary>
        public async Task<List<DimProductDTO>> TransformarProductosAsync()
        {
            var productosOrigen = await ObtenerProductosAsync();
            var resultado = new List<DimProductDTO>();

            foreach (var p in productosOrigen)
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

            LoggerHelper.Info($"Productos transformados: {resultado.Count}");
            return resultado;
        }

        /// <summary>
        /// Transforma clientes a DimCustomerDTO.
        /// </summary>
        public async Task<List<DimCustomerDTO>> TransformarClientesAsync()
        {
            var clientesOrigen = await ObtenerClientesAsync();
            var resultado = new List<DimCustomerDTO>();

            foreach (var c in clientesOrigen)
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

            LoggerHelper.Info($"Clientes transformados: {resultado.Count}");
            return resultado;
        }

        /// <summary>
        /// Transforma ventas a una fact table temporal.
        /// </summary>
        public async Task<List<FactSalesDTO>> TransformarVentasAsync()
        {
            var ventasOrigen = await ObtenerVentasAsync();
            var resultado = new List<FactSalesDTO>();

            foreach (var v in ventasOrigen)
            {
                if (!_ventaValidator.EsValido(v))
                    continue;

                resultado.Add(new FactSalesDTO
                {
                    ProductKey = v.OrderID.GetHashCode(),     
                    CustomerKey = v.CustomerID.GetHashCode(),
                    DateKey = v.OrderDate ?? DateTime.UtcNow,
                    SourceKey = 1,                            
                    Quantity = 1,                             
                    UnitPrice = 0,                           
                    Discount = 0
                });
            }

            LoggerHelper.Info($"Ventas transformadas: {resultado.Count}");
            return resultado;
        }


        // ===============================================================
        //   MÉTODOS TEMPORALES (SE REEMPLAZAN EN EXTRACT)
        // ===============================================================

        private async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            await Task.Delay(5);
            return new List<ClienteDTO>(); // se llenará desde ExtractionService
        }

        private async Task<List<ProductoDTO>> ObtenerProductosAsync()
        {
            await Task.Delay(5);
            return new List<ProductoDTO>(); // se llenará desde ExtractionService
        }

        private async Task<List<VentaDTO>> ObtenerVentasAsync()
        {
            await Task.Delay(5);
            return new List<VentaDTO>(); // se llenará desde ExtractionService
        }
    }
}
