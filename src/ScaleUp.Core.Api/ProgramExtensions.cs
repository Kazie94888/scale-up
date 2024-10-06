using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using ScaleUp.Core.Api.Base.Behaviors;
using ScaleUp.Core.Api.Base.Configurations;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Webhooks.Haravan;
using ScaleUp.Core.Application;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Application.Orders.Validations;
using ScaleUp.Core.SharedKernel.Configurations;
using ScaleUp.Integrations.Haravan;
using ScaleUp.Integrations.Haravan.Base.ValueObjects;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api;

internal static class ProgramExtensions
{
    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyResolver>();

            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
    }

    internal static void AddApiDocs(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(document =>
        {
            document.Title = "Scaleup API - Back Office";
            document.SchemaSettings.GenerateEnumMappingDescription = true;

            document.PostProcess = d =>
            {
                d.Info.TermsOfService = "Term of service";
                d.Info.Version = "v1.0";
                d.Info.License = new OpenApiLicense
                {
                    Url = "http://www.apache.org/licenses/LICENSE-2.0.html",
                    Name = "Apache 2.0"
                };
            };
            document.AddSecurity("openId", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = "https://localhost/.well-known/openid-configuration", //TODO: config OpenIdConnectUrl
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Standard authorisation using the Bearer scheme. Example: \"bearer {token}\"",
            });
        });
    }

    internal static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();
    }

    internal static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });
    }

    internal static void AddJsonOptions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.KebabCaseLower, allowIntegerValues: false));
        });

        builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    }

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("Base/Configurations/global-configuration.json", true, true);
        builder.Configuration.AddJsonFile("Base/Configurations/order-cancel-reason-configurations.json", true, true);
        builder.Configuration.AddJsonFile("Base/Configurations/feature-permissions.json", true, true);

        var globalConfigurationSection = builder.Configuration.GetSection(nameof(GlobalConfigurations));
        var globalConfiguration = globalConfigurationSection.Get<GlobalConfigurations>();
        if (globalConfiguration != null) builder.Services.AddSingleton(globalConfiguration);

        var orderCancelReasonConfigurations = builder.Configuration.GetSection(nameof(OrderCancelReasonConfigurations)).Get<OrderCancelReasonConfigurations>();
        if (orderCancelReasonConfigurations != null) builder.Services.AddSingleton(orderCancelReasonConfigurations);

        var featurePermissionConfigurations = builder.Configuration.GetSection(nameof(FeaturePermissionConfigurations)).Get<FeaturePermissionConfigurations>();
        if (featurePermissionConfigurations != null) builder.Services.AddSingleton(featurePermissionConfigurations);
    }

    internal static void AddIntegrations(this WebApplicationBuilder builder)
    {
        var haravanIntegrationSettings = builder.Configuration.GetSection(nameof(HaravanIntegrationSettings)).Get<HaravanIntegrationSettings>();
        builder.Services.AddHaravanIntegration(haravanIntegrationSettings?.BaseAddress, haravanIntegrationSettings?.SecretKey);
        builder.Services.AddSingleton(haravanIntegrationSettings!);
    }

    internal static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderActionValidator, OrderActionValidator>();
        builder.Services.AddScoped<IOrderManager, OrderManager>();
    }

    internal static void UseMiddlewares(this WebApplication app)
    {
        // TODO: Enable this after implementing authen & autho
        // app.UseMiddleware<TenantMiddleware>();
    }

    internal static void AddHttpContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }

    internal static void AddWebHooks(this WebApplicationBuilder builder)
    {
        builder.Services.AddWebHook<HaravanWebHook>();
    }
}
