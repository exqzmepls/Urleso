using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Urleso.Redirect.Persistence;

internal sealed class NpgsqlConnectionFactory(
    IOptions<ConnectionStrings> connectionStringsOptions
)
    : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connectionString = connectionStringsOptions.Value.Postgresql;
        return new NpgsqlConnection(connectionString);
    }
}