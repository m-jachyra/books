using System.Linq.Expressions;
using Backend.Helpers;

namespace Backend.Services.Base
{
    public interface IServiceAsync<TEntity, TDto>
    {
        Task AddAsync(TDto tDto);
        Task DeleteAsync(int id);
        Task<PagedList<TDto>> GetAsync(PagedListQuery<TEntity> request);
        Task<PagedList<TDto>> GetAsync(Expression<Func<TDto, bool>>? expression, PagedListQuery<TEntity> request);
        Task<TDto> GetByIdAsync(int id);
        Task UpdateAsync(TDto tDto);
        Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);
    }
}