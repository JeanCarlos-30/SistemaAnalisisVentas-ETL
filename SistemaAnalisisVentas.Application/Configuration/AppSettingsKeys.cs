using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Configuration
{
    /// <summary>
    /// Contiene las claves principales utilizadas para obtener valores del archivo de configuración
    /// (appsettings.json o variables de entorno). Centraliza el acceso a configuración global.
    /// </summary>
    public static class AppSettingsKeys
    {
        //Claves generales del proceso ETL
        public const string ConnectionStringKey = "ConnectionStrings:SalesDWDatabase";
        public const string ApiBaseUrlKey = "ExternalSources:ApiBaseUrl";
        public const string CsvFolderPathKey = "ExternalSources:CsvFolderPath";

        //Claves de fuentes individuales
        public const string ProductsCsvFile = "ExternalSources:CsvFiles:Products";
        public const string CustomersCsvFile = "ExternalSources:CsvFiles:Customers";
        public const string SalesCsvFile = "ExternalSources:CsvFiles:Sales";

        //Endpoints de API
        public const string ProductsApiEndpoint = "ExternalSources:ApiEndpoints:Products";
        public const string CustomersApiEndpoint = "ExternalSources:ApiEndpoints:Customers";

        //Parámetros del ETL
        public const string EtlBatchSizeKey = "ETL:BatchSize";
        public const string EtlRetryCountKey = "ETL:RetryCount";
        public const string EtlEnableLoggingKey = "ETL:EnableLogging";

        //Claves para Power BI o Dashboards (opcional)
        public const string ReportingApiBaseUrl = "Reporting:ApiBaseUrl";
    }
}

