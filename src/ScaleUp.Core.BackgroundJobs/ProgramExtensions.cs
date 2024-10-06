using FluentValidation;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using HangfireBasicAuthenticationFilter;
using ScaleUp.Core.Application;
using ScaleUp.Core.Application.Integrations.Haravan;
using ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;
using ScaleUp.Core.Application.Integrations.Haravan.Features.Products;
using ScaleUp.Core.BackgroundJobs.Haravan;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Integrations.Haravan;
using ScaleUp.Integrations.Haravan.Base.ValueObjects;
using System.Reflection;
using ScaleUp.Core.Application.Integrations;

namespace ScaleUp.Core.BackgroundJobs;

internal static class ProgramExtensions
{
    internal static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IHaravanJobManager, HaravanJobManager>();
        builder.Services.AddScoped<HaravanProductService>();
        builder.Services.AddScoped<HaravanOrderService>();
        builder.Services.AddScoped<IHaravanIntegrationService, HaravanIntegrationService>();
    }

    internal static void AddHangfire(this WebApplicationBuilder builder)
    {
        var mongoConnection = builder.Configuration.GetConnectionString("Default");
        var migrationOptions = new MongoMigrationOptions
        {
            MigrationStrategy = new MigrateMongoMigrationStrategy(),
            BackupStrategy = new CollectionMongoBackupStrategy(),
        };

        builder.Services.AddHangfire(config =>
        {
            config.UseFilter(new AutomaticRetryAttribute { Attempts = 0 });
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();
            config.UseMongoStorage(mongoConnection, DbConstants.ScaleUpDbName, new MongoStorageOptions { MigrationOptions = migrationOptions });

        });
        builder.Services.AddHangfireServer();
    }

    internal static void ConfigureHangfireDashboard(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Scaleup Hangfire Dashboard",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = "scaleup-dev",
                    Pass = "12345678"
                }
            }
        });
    }

    internal static void AddIntegrations(this WebApplicationBuilder builder)
    {
        var haravanIntegrationSettings = builder.Configuration.GetSection(nameof(HaravanIntegrationSettings)).Get<HaravanIntegrationSettings>();
        builder.Services.AddHaravanIntegration(haravanIntegrationSettings?.BaseAddress, haravanIntegrationSettings?.SecretKey);
        builder.Services.AddSingleton(haravanIntegrationSettings!);
    }

    internal static void AddRecurringJobs(this WebApplication _)
    {
        HaravanJobScheduler.Schedule();
    }

    internal static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyResolver>();
        });
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
    }
    
    internal static void AddHttpContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }
}
