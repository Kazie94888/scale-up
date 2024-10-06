using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.Persistence.DomainEvents;
using ScaleUp.Core.SharedKernel.Constants;

namespace ScaleUp.Core.Persistence;

public static class ProgramExtensions
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MasterDataContext>((_, options) =>
        {
            options.UseMongoDB(builder.Configuration.GetConnectionString("Default")!, DbConstants.ScaleUpDbName);
        });

        builder.Services.AddDbContext<HaravanDataContext>((_, options) =>
        {
            options.UseMongoDB(builder.Configuration.GetConnectionString("Default")!, DbConstants.ScaleUpDbName);
        });

        builder.Services.AddScoped<ReadOnlyMasterDataContext>();
        builder.Services.AddScoped<MasterDataContext>();
        builder.Services.AddScoped<HaravanDataContext>();

        builder.Services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
    }
}
