using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Urleso.DatabaseMigrator;
using Urleso.Persistence;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

services.AddDatabase(builder.Configuration);
services.AddHostedService<MigratorHostedService>();

var host = builder.Build();

await host.StartAsync();
await host.StopAsync();