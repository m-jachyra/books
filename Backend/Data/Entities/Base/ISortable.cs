using System.Linq.Expressions;

namespace Backend.Data.Entities
{
    public interface ISortable<T>
    {
        public static abstract Expression<Func<T, object>> GetSortProperty(string? sortColumn);
    }
}