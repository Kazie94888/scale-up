using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ScaleUp.Core.SharedKernel.Caching;
using ScaleUp.Core.SharedKernel.Constants;
using Serilog;

namespace ScaleUp.Core.SharedKernel.Extensions;

public static class ProgramExtensions
{
    public static void AddCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICacheService, CacheService>();
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    }

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory());
        configBuilder.AddJsonFile("appsettings.json");
        configBuilder.AddJsonFile($"appsettings.{environmentName}.json", true);
        configBuilder.AddEnvironmentVariables();

        builder.Configuration.AddConfiguration(configBuilder.Build());
    }

    public static void AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
    }

    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
                        .AddMongoDb(mongodbConnectionString: builder.Configuration.GetConnectionString("Default"),
                                    mongoDatabaseName: DbConstants.ScaleUpDbName,
                                    failureStatus: HealthStatus.Unhealthy);
    }

    public static void MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}
