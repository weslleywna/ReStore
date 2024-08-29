using API.Data.Config;

namespace API.Data.Repositories.Base
{
    public abstract class DbReStoreRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class
    {
        public DbReStoreRepositoryBase(IDbConnectionFactory dbConnectionFactory) : base (dbConnectionFactory, DatabaseConnectionNameEnum.DbReStore)
        {
           
        }
    }
}