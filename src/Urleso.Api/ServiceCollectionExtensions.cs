namespace Urleso.Api;

internal static class ServiceCollectionExtensions
{
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