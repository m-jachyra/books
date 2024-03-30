using System.Linq.Expressions;
using Backend.Data;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class, IHasId
    {
        private readonly AppDbContext _context;
        
        public RepositoryAsync(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null)
        {
            var result = _context.Set<T>().AsNoTracking();
            if (expression != null)
                result = result.Where(expression);
            return result;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> Entities => _context.Set<T>();
    }
}