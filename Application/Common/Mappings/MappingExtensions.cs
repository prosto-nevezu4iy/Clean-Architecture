using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this IEnumerable<TDestination> queryable, int count, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.Create(queryable, count, pageNumber, pageSize);
    }
}
