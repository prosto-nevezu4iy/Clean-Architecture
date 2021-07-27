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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .OrderBy(x => x.Name)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
