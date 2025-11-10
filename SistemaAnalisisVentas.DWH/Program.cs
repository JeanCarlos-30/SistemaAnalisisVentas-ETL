using SistemaAnalisisVentas.DWH;
using SistemaAnalisisVentas.IoC;

public static class Program
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSistemaAnalisisVentas(context.Configuration);
                services.AddHostedService<Worker>();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });

        var host = builder.Build();

        //  Guardamos el ServiceProvider global para usarlo en el Worker
        ServiceProvider = host.Services;

        await host.RunAsync();
    }
}
