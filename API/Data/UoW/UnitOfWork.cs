using API.Data.Repositories.Base;

namespace API.Data.UoW
{
    public sealed class UnitOfWork(DbSessionReStoreRepositoryBase session) : IUnitOfWork
    {
        private readonly DbSessionReStoreRepositoryBase _session = session;

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction?.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}