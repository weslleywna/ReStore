using API.Models;

namespace API.Data.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(Guid id);
        Task<Guid> Insert(TEntity entity);
        Task<TEntity> InsertAndFetch(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(Guid id);
    }
}