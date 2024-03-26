using System.Linq.Expressions;
using Backend.Data.Entities;

namespace Backend.Repositories
{
    public interface IRepositoryAsync<T> where T : Entity
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null);
        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Entities { get; }
    }
}