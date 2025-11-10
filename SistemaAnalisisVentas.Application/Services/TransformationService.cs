using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.OperationResults;
using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Validators;

namespace SistemaAnalisisVentas.Application.Services
{
    /// <summary>
    /// Servicio encargado de transformar y validar los datos extraídos.
    /// Limpia, normaliza, aplica reglas de negocio y prepara los datos para el DWH.
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

        // ---------------------------------
        // 🔹 1. Transformar y validar
        // ---------------------------------
        public async Task TransformAndValidateAsync()
        {
            LoggerHelper.Info("Iniciando transformación y validación...");

            try
            {
                var clientes = await ObtenerClientesAsync();
                var productos = await ObtenerProductosAsync();
                var ventas = await ObtenerVentasAsync();

                /*clientes = clientes.Where(_clienteValidator.EsValido).ToList();
                productos = productos.Where(_productoValidator.EsValido).ToList();
                ventas = ventas.Where(_ventaValidator.EsValido).ToList();*/

                LoggerHelper.Info($"Clientes válidos: {clientes.Count}, Productos válidos: {productos.Count}, Ventas válidas: {ventas.Count}");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("Error durante la transformación o validación de datos.", ex);
            }

            LoggerHelper.Info("Transformación completada.");
        }

        // ---------------------------------
        // 🔹 2. Calcular valores derivados
        // ---------------------------------
        public async Task ComputeDerivedValuesAsync()
        {
            LoggerHelper.Info("Calculando valores derivados...");
            await Task.Run(() =>
            {
                // Aquí podrías calcular descuentos, totales o métricas intermedias
            });
            LoggerHelper.Info("Cálculo de valores derivados completado.");
        }

        // Métodos simulados: luego vendrán de la fase Extract
        private async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            await Task.Delay(50);
            return new List<ClienteDTO>();
        }

        private async Task<List<ProductoDTO>> ObtenerProductosAsync()
        {
            await Task.Delay(50);
            return new List<ProductoDTO>();
        }

        private async Task<List<VentaDTO>> ObtenerVentasAsync()
        {
            await Task.Delay(50);
            return new List<VentaDTO>();
        }
    }
}
