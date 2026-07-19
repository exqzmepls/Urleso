using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Urleso.Api;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebCors(this IServiceCollection services)
    {
        services.AddOptions<CorsSettings>()
            .BindConfiguration(CorsSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddCors();
        services.AddOptions<CorsOptions>()
            .Configure<IOptions<CorsSettings>>((corsOptions, corsSettingsOptions) =>
            {
                var allowedOrigins = corsSettingsOptions.Value.AllowedOrigins
                    .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(origin => origin.TrimEnd('/'))
                    .ToArray();

                corsOptions.AddDefaultPolicy(policy => policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

        return services;
    }

    public static IServiceCollection AddOpenApiGen(this IServiceCollection services)
    {
        services.AddOpenApiDocument(settings =>
        {
            var assembly = typeof(Program).Assembly;
            var assemblyName = assembly.GetName();
            settings.Title = assemblyName.Name;
        });

        return services;
    }
}