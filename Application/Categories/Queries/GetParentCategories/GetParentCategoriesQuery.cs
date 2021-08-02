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
        private readonly ICategoryQueries _queries;
        private readonly IMapper _mapper;

        public GetParentCategoriesQueryHandler(ICategoryQueries queries, IMapper mapper)
        {
            _queries = queries;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _queries.GetParentCategories();

            return categories
                .AsQueryable()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
