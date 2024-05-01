using Serilog;
using Urleso.Redirect.Application;
using Urleso.Redirect.Persistence;
using Urleso.Redirect.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
services.AddApplication();
services.AddPersistence();
services.AddProblemDetails();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.MapShortenedUrlRedirect();

app.Run();