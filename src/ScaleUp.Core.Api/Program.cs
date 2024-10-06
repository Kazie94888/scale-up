using ScaleUp.Core.Api;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Persistence;
using ScaleUp.Core.SharedKernel.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();
builder.AddJsonOptions();
builder.AddConfiguration();
builder.AddCustomConfiguration();
builder.AddApiDocs();
builder.AddMediatR();
builder.AddDependencies();
builder.AddDatabase();
builder.AddIntegrations();
builder.AddHttpContext();
builder.AddWebHooks();

builder.AddCors();
builder.AddAuthorization();
builder.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpoints();
builder.Services.AddOutputCache();
builder.AddHealthChecks();

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseOpenApi();
    app.UseReDoc();
}

app.UseCors();
app.UseOutputCache();

app.UseAuthentication();
app.UseMiddlewares();
app.UseAuthorization();

app.MapEndpoints();
app.UseSerilogRequestLogging();
app.MapHealthChecks();

app.Run();
