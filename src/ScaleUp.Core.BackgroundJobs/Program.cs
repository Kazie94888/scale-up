using ScaleUp.Core.BackgroundJobs;
using ScaleUp.Core.Persistence;
using ScaleUp.Core.SharedKernel.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();
builder.AddMediatR();
builder.AddHangfire();
builder.AddDatabase();
builder.AddCache();
builder.AddDependencyInjection();
builder.AddIntegrations();
builder.AddHealthChecks();
builder.AddConfiguration();
builder.AddHttpContext();

var app = builder.Build();
app.MapHealthChecks();
app.ConfigureHangfireDashboard();
app.UseSerilogRequestLogging();
app.AddRecurringJobs();
app.Run();
