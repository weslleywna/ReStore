using System.Data;
using API.Data.Config;
using Npgsql;

namespace API.Data.Factory
{
    public class DapperDbConnectionFactory(IDictionary<DatabaseConnectionNameEnum, string> connectionDict) : IDbConnectionFactory
    {
        private readonly IDictionary<DatabaseConnectionNameEnum, string> _connectionDict = connectionDict;

        public IDbConnection CreateDbConnection(DatabaseConnectionNameEnum connectionName)
        {
            if (_connectionDict.TryGetValue(connectionName, out string? connectionString))
            {
                return new NpgsqlConnection(connectionString);
            }

            throw new ArgumentNullException();
        }
    }
}