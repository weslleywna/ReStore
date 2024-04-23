using System.Data;

namespace API.Data.Config
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(DatabaseConnectionNameEnum connectionName);
    }
}