using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Helpers
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize, Expression<Func<T, object>>? sortRequest = null)
        {
            var totalCount = await query.CountAsync();

            if (sortRequest != null)
                query = query.OrderBy(sortRequest);
            
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new(items, page, pageSize, totalCount);
        }
    }

    public record PagedListQuery<T>(int Page = 1, int PageSize = 10, string SortOrder = "asc", string SortColumn = "id");
}