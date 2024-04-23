using System.Data;
using API.Data.Config;

namespace API.Data.Repositories.Base
{
    public abstract class DbReStoreRepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }

        public DbReStoreRepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            // Now it's the time to pick the right connection string!
            // Enum is used. No magic string!
            DbConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionNameEnum.DbReStore);
        }
    }
}