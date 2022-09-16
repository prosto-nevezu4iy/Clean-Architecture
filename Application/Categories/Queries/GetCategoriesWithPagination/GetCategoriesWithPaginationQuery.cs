using Application.Categories.Queries.Dtos;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategoriesWithPagination
{
    public class GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
    {
        private readonly ICategoryQueries _queries;
        private readonly IMapper _mapper;

        public GetCategoriesWithPaginationQueryHandler(ICategoryQueries queries, IMapper mapper)
        {
            _queries = queries;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _queries.GetCategories(request.PageNumber, request.PageSize);

            return pagedResult.Items
                .AsQueryable()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToPaginatedList(pagedResult.Count, request.PageNumber, request.PageSize);
        }
    }
}
