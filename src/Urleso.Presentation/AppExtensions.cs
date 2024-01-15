using Carter;
using Microsoft.AspNetCore.Builder;
using Urleso.Presentation.RedirectShortenedUrl;

namespace Urleso.Presentation;

public static class AppExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapShortenedUrlRedirect();
        app.MapCarter();

        return app;
    }
}