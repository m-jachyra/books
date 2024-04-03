using System.Linq.Expressions;
using Backend.Helpers;

namespace Backend.Services.Base
{
    public interface IServiceAsync<TEntity, TListDto, TDetailsDto, TUpdateDto>
    {
        Task AddAsync(TUpdateDto tDto);
        Task DeleteAsync(int id);
        Task<PagedList<TListDto>> GetAsync(PagedListQuery<TEntity> request);
        Task<PagedList<TListDto>> GetAsync(IQueryable<TEntity> query, PagedListQuery<TEntity> request);
        Task<TDetailsDto> GetByIdAsync(int id);
        Task<TDetailsDto> GetByIdAsync(IQueryable<TEntity> query, int id);
        Task UpdateAsync(TUpdateDto tDto);
        Task<TDetailsDto> GetFirstAsync(Expression<Func<TEntity, bool>> expression);
    }
}