

using System.Linq;
using Object13.Core.DTOs.Paging;

namespace Object13.Core.Utilites.Extention
{
    public static class PagingExtentions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable , BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
