
using Serilog;
using WarehouseManager.Application;
using WarehouseManager.Host.Configurations;
using WarehouseManager.Host.Controllers;
using WarehouseManager.Infrastructure;
using WarehouseManager.Infrastructure.Common;
using WarehouseManager.Infrastructure.Logging.Serilog;

namespace WarehouseManager.Api
{
    [ApiConventionType(typeof(WHMApiConventions))]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            StaticLogger.EnsureInitialized();
            Log.Information("Server Booting Up...");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.AddConfigurations().RegisterSerilog();
                builder.Services.AddControllers();
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddApplication();

                var app = builder.Build();

                await app.Services.InitializeDatabasesAsync();

                app.UseInfrastructure(builder.Configuration);
                app.MapEndpoints();
                app.Run();
            }
            catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                StaticLogger.EnsureInitialized();
                Log.Fatal(ex, "Unhandled exception");
            }
            finally
            {
                StaticLogger.EnsureInitialized();
                Log.Information("Server Shutting down...");
                Log.CloseAndFlush();
            }
        }
    }
}
