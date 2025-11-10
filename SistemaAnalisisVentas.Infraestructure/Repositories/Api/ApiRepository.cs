using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Api
{
    public class ApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiRepository> _logger;

        public ApiRepository(HttpClient httpClient, ILogger<ApiRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<T>> ObtenerDatosAsync<T>(string url)
        {
            var resultados = new List<T>();

            try
            {
                _logger.LogInformation(" Iniciando consumo de API: {Url}", url);

                if (string.IsNullOrWhiteSpace(url))
                {
                    _logger.LogWarning(" URL de API no especificada.");
                    return resultados;
                }

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(" Error en la solicitud HTTP ({Codigo}): {Mensaje}",
                        (int)response.StatusCode, response.ReasonPhrase);
                    return resultados;
                }

                var data = await response.Content.ReadFromJsonAsync<List<T>>();
                resultados = data ?? new List<T>();

                _logger.LogInformation(" Consumo exitoso: {Cantidad} registros obtenidos desde {Url}",
                    resultados.Count, url);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, " Error de conexión al consumir la API ({Url})", url);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogError(ex, " El contenido recibido no tiene un formato esperado ({Url})", url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error inesperado al consumir la API ({Url})", url);
            }

            return resultados;
        }
    }
}

