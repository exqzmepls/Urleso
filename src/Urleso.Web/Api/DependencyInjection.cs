using Microsoft.Extensions.Options;
using Urleso.Web.Api.ShortenedUrls;

namespace Urleso.Web.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddApiSettings(this IServiceCollection services)
    {
        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IShortenedUrlService, ShortenedUrlService>();

        services.AddSingleton<IApiClientFactory, ApiHttpClientFactory>();
        services.AddHttpClient(ApiHttpClientFactory.HttpClientName, (serviceProvider, client) =>
        {
            var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(apiSettings.BaseAddress);
        });

        return services;
    }
}