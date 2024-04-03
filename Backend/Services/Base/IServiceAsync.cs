using System.Linq.Expressions;
using Backend.Helpers;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Backend.Services.Base
{
    public interface IServiceAsync<TEntity, TListDto, TDetailsDto, TUpdateDto>
    {
        Task AddAsync(TUpdateDto tDto);
        Task DeleteAsync(int id);
        Task<List<TListDto>> GetMappedListAsync(IQueryable<TEntity> query, IConfigurationProvider? configurationProvider = null);
        Task<PagedList<TListDto>> GetMappedPagedListAsync(PagedListQuery<TEntity> request, IQueryable<TEntity>? query = null, IConfigurationProvider? configurationProvider = null);
        Task<TDetailsDto> GetByIdAsync(int id, IQueryable<TEntity>? query = null, IConfigurationProvider? configurationProvider = null);
        Task UpdateAsync(TUpdateDto tDto);
        Task<TDetailsDto> GetFirstAsync(Expression<Func<TEntity, bool>> expression);
    }
}