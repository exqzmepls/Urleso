using Serilog;
using Urleso.Application;
using Urleso.Infrastructure;
using Urleso.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
services.AddApplication();
services.AddInfrastructure(builder.Configuration);
services.AddPresentation();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.MapEndpoints();

app.Run();