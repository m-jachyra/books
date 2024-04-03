using System.Linq.Expressions;
using Backend.Data.Entities;

namespace Backend.Repositories
{
    public interface IRepositoryAsync<T> where T : class, IHasId
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Entities { get; }
    }
}