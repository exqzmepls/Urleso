using System.Data;

namespace Urleso.Redirect.Persistence;

public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection();
}