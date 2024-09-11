using API.Customs.Exceptions;
using Dommel;

namespace API.Data.Repositories.Base
{
    public abstract class RepositoryBase<TEntity>(DbSessionReStoreRepositoryBase session) where TEntity : class
    {
        protected DbSessionReStoreRepositoryBase _session = session;

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await _session.Connection.GetAllAsync<TEntity>();
        }

        public async virtual Task<TEntity?> GetById(Guid id)
        {
            return await _session.Connection.GetAsync<TEntity>(id);
        }

        public async virtual Task<Guid> Insert(TEntity entity)
        {
            var id = await _session.Connection.InsertAsync(entity);
            return (Guid)id;
        }

        public async virtual Task<TEntity> InsertAndFetch(TEntity entity)
        {
            var id = await _session.Connection.InsertAsync(entity);
            var insertedEntity = await GetById((Guid)id);
            return insertedEntity!;
        }

        public async virtual Task<bool> Update(TEntity entity)
        {
            return await _session.Connection.UpdateAsync(entity);
        }

        public async virtual Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id) ?? throw new NotFoundException("Entity was not found");
            return await _session.Connection.DeleteAsync(entity);
        }
    }
}