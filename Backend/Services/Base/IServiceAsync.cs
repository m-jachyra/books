using System.Linq.Expressions;

namespace Backend.Services.Base
{
    public interface IServiceAsync<TEntity, TDto>
    {
        Task AddAsync(TDto tDto);
        Task DeleteAsync(int id);
        IEnumerable<TDto> GetAll(Expression<Func<TDto, bool>>? expression = null);
        Task<TDto> GetByIdAsync(int id);
        Task UpdateAsync(TDto tDto);
        Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);
    }
}