using Application.Categories.Queries.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryQueries _queries;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryQueries queries, IMapper mapper)
        {
            _queries = queries;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _queries.GetCategoryById(request.Id);

            if(category == null)
            {
                return default;
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId
            };
        }
    }
}
