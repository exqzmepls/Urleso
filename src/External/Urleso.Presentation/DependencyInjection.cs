using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace Urleso.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var presentationAssembly = typeof(DependencyInjection).Assembly;
        var assemblyCatalog = new DependencyContextAssemblyCatalog(new[]
        {
            presentationAssembly
        });
        services.AddCarter(assemblyCatalog);

        return services;
    }
}