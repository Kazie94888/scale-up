using Microsoft.Extensions.DependencyInjection;
using Refit;
using ScaleUp.Integrations.Haravan.ApiConsumers;

namespace ScaleUp.Integrations.Haravan;

public static class ProgramExtensions
{
    public static void AddHaravanIntegration(this IServiceCollection services, string baseAddress, string secretKey)
    {
        services.AddRefitClient<IHaravanOrderHubApi>()
            .ConfigureHttpClient(client => ConfigureHttpClient(client, baseAddress, secretKey));

        services.AddRefitClient<IHaravanProductHubApi>()
            .ConfigureHttpClient(client => ConfigureHttpClient(client, baseAddress, secretKey));
    }
    
    private static void ConfigureHttpClient(HttpClient client, string baseAddress, string secretKey)
    {
        client.BaseAddress = new Uri(baseAddress);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
    }
}