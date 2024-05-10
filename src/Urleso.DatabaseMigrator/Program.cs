using Steeltoe.Extensions.Configuration.Placeholder;
using Urleso.DatabaseMigrator;
using Urleso.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .AddPlaceholderResolver()
    .ConfigureServices(services => services
        .AddDatabase()
        .AddSingleton<MigrationsWorker>()
        .AddHostedService<MigratorHostedService>()
    )
    .Build();

host.Run();