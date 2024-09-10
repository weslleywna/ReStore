using System.Data;
using API.Customs.Exceptions;
using API.Data.Config;
using Dommel;

namespace API.Data.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        public IDbConnection DbConnection { get; private set; }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, DatabaseConnectionNameEnum databaseEnum)
        {
            DbConnection = dbConnectionFactory.CreateDbConnection(databaseEnum);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            using var db = DbConnection;
            return await db.GetAllAsync<TEntity>();
        }

        public async virtual Task<TEntity?> GetById(Guid id)
        {
            using var db = DbConnection;
            return await db.GetAsync<TEntity>(id);
        }

        public async virtual Task<Guid> Insert(TEntity entity)
        {
            using var db = DbConnection;
            var id = await db.InsertAsync(entity);
        
            return (Guid)id;
        }

        public async virtual Task<TEntity> InsertAndFetch(TEntity entity)
        {
            using var db = DbConnection;
            var id = await db.InsertAsync(entity);

            var insertedEntity = await db.GetAsync<TEntity>(id);
        
            return insertedEntity!;
        }

        public async virtual Task<bool> Update(TEntity entity)
        {
            using var db = DbConnection;
            return await db.UpdateAsync(entity);
        }

        public async virtual Task<bool> Delete(Guid id)
        {
            using var db = DbConnection;
            
            var entity = await db.GetAsync<TEntity>(id);
 
            if (entity == null) throw new NotFoundException("Entity was not found");
 
            return await db.DeleteAsync(entity);
        }
    }
}