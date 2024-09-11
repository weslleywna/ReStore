using System.Data;
using API.Data.Config;

namespace API.Data.Repositories.Base
{
    public sealed class DbSessionReStoreRepositoryBase : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DbSessionReStoreRepositoryBase(IDbConnectionFactory dbConnectionFactory, DatabaseConnectionNameEnum databaseEnum)
        {
            Connection = dbConnectionFactory.CreateDbConnection(databaseEnum);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}