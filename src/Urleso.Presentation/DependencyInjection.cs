using Carter;
using Microsoft.Extensions.DependencyInjection;
using Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;

namespace Urleso.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddOptions<RedirectSettings>()
            .BindConfiguration(RedirectSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var presentationAssembly = typeof(DependencyInjection).Assembly;
        var assemblyCatalog = new DependencyContextAssemblyCatalog(new[]
        {
            presentationAssembly
        });
        services.AddCarter(assemblyCatalog);

        return services;
    }
}