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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

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
