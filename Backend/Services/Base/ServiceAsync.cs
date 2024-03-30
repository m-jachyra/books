using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models.Base;
using Backend.Repositories;

namespace Backend.Services.Base
{
    public class ServiceAsync<TEntity, TDto> : IServiceAsync<TEntity, TDto>
        where TDto : EntityDto where TEntity : class, IHasId, ISortable<TEntity>
    {
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        
        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
        }
        
        public async Task<PagedList<TDto>> GetAsync(PagedListQuery<TEntity> request)
        {
            var query = _repository.GetAll();
            
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(TEntity.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(TEntity.GetSortProperty(request.SortColumn));
            
            var result = query.ProjectTo<TDto>(_mapper.ConfigurationProvider);
            return await PagedList<TDto>.CreateAsync(result, request.Page, request.PageSize);
        }

        public async Task<PagedList<TDto>> GetAsync(Expression<Func<TDto, bool>>?expression, PagedListQuery<TEntity> request)
        {
            var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);

            var query = _repository.GetAll(predicate);
            
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(TEntity.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(TEntity.GetSortProperty(request.SortColumn));
            
            var result = query.ProjectTo<TDto>(_mapper.ConfigurationProvider);
            return await PagedList<TDto>.CreateAsync(result, request.Page, request.PageSize);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task UpdateAsync(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.UpdateAsync(entity);
        }

        public async Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression)
        {
            var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
            var entity = await _repository.GetFirstAsync(predicate);
            return _mapper.Map<TDto>(entity);
        }
    }
}