using Serilog;
using Urleso.Api;
using Urleso.Application;
using Urleso.Infrastructure;
using Urleso.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
services.AddApplication();
services.AddInfrastructure();
services.AddPresentation();
services.AddEndpointsApiExplorer();
services.AddOpenApiGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseSerilogRequestLogging();
app.MapEndpoints();

app.Run();