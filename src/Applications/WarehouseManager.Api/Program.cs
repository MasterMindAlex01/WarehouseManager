using FluentValidation;
using Serilog;
using System.Reflection;
using WarehouseManager.Host.Configurations;
using WarehouseManager.Infrastructure;
using WarehouseManager.Infrastructure.Common;
using WarehouseManager.Infrastructure.Logging.Serilog;
using WarehouseManager.Infrastructure.Validations;

namespace WarehouseManager.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        StaticLogger.EnsureInitialized();
        Log.Information("Server Booting Up...");
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var assembly = Assembly.GetExecutingAssembly();

            builder.AddConfigurations().RegisterSerilog();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly))
            .AddValidatorsFromAssembly(assembly)
            .AddBehaviours(assembly);

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
