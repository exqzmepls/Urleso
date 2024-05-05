namespace Urleso.Redirect;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
            };
        });

        return services;
    }
}