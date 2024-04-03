using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models.Base;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Base
{
    public class ServiceAsync<TEntity, TListDto, TDetailsDto, TUpdateDto> : IServiceAsync<TEntity, TListDto, TDetailsDto, TUpdateDto>
        where TEntity : class, IHasId, ISortable<TEntity>
        where TListDto : IListDto
        where TDetailsDto : IDetailsDto
        where TUpdateDto : IUpdateDto
    {
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        
        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(TUpdateDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
        }
        
        public async Task<PagedList<TListDto>> GetAsync(PagedListQuery<TEntity> request)
        {
            var query = _repository.GetAll();
            
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(TEntity.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(TEntity.GetSortProperty(request.SortColumn));
            
            var result = query.ProjectTo<TListDto>(_mapper.ConfigurationProvider);
            return await PagedList<TListDto>.CreateAsync(result, request.Page, request.PageSize);
        }

        public async Task<PagedList<TListDto>> GetAsync(IQueryable<TEntity> query, PagedListQuery<TEntity> request)
        {
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(TEntity.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(TEntity.GetSortProperty(request.SortColumn));
            
            var result = query.ProjectTo<TListDto>(_mapper.ConfigurationProvider);
            return await PagedList<TListDto>.CreateAsync(result, request.Page, request.PageSize);
        }
        
        public async Task<List<TListDto>> GetAsync(IQueryable<TEntity> query)
        {
            var result = query.ProjectTo<TListDto>(_mapper.ConfigurationProvider);
            return await result.ToListAsync();
        }

        public async Task<TDetailsDto> GetByIdAsync(int id)
        {
            var query = _repository.Entities;
            var result = query.ProjectTo<TDetailsDto>(_mapper.ConfigurationProvider);
            return await result.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<TDetailsDto> GetByIdAsync(IQueryable<TEntity> query, int id)
        {
            var result = query.ProjectTo<TDetailsDto>(_mapper.ConfigurationProvider);
            return await result.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(TUpdateDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.UpdateAsync(entity);
        }

        public async Task<TDetailsDto> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
            var entity = await _repository.GetFirstAsync(predicate);
            return _mapper.Map<TDetailsDto>(entity);
        }
    }
}