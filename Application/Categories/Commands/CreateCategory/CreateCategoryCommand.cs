using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
