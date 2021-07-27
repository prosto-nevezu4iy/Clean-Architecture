using Application.Categories.Queries.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategories
{
    public class GetParentCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }

    public class GetParentCategoriesQueryHandler : IRequestHandler<GetParentCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetParentCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(c => c.ParentId == null)
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
