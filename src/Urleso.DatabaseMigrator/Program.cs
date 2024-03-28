using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Urleso.DatabaseMigrator;
using Urleso.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services
        .AddDatabase()
        .AddSingleton<MigrationsWorker>()
        .AddHostedService<MigratorHostedService>()
    )
    .Build();

await host.RunAsync();