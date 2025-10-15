using System.Linq.Expressions;

namespace Transfer_ListerAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeEntities = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>>? filter = null, string? includeEntities = null);
        Task Create(T entity);
        Task Delete(T entity);
        Task SaveAsync();
    }
}
