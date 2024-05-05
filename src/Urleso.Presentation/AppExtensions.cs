using Carter;
using Microsoft.AspNetCore.Builder;

namespace Urleso.Presentation;

public static class AppExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}