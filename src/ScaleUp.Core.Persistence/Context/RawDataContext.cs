// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
// using Microsoft.Extensions.DependencyInjection;
// using ScaleUp.Core.Persistence.Conventions;
// using ScaleUp.Core.Persistence.Extensions;
//
// namespace ScaleUp.Core.Persistence.Context;
//
// public sealed class RawDataContext : DbContext
// {
//     public RawDataContext(DbContextOptions<RawDataContext> options) : base(options)
//     {
//     }
//
//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
//         builder.ApplyBaseEntityProperties();
//         base.OnModelCreating(builder);
//     }
//
//     protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
//     {
//         configurationBuilder.Conventions.Add(serviceProvider =>
//             new CamelCaseElementNameConvention(serviceProvider
//                 .GetRequiredService<ProviderConventionSetBuilderDependencies>()));
//     }
// }