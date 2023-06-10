using Persistence;
using MediatR;
using Application.Core;

namespace Application.Recipes;

public class DeleteRecipe
{
    public class Command : IRequest<RequestResult<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, RequestResult<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context) => _context = context;

        async Task<RequestResult<Unit>> IRequestHandler<Command, RequestResult<Unit>>.Handle(Command request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes.FindAsync(request.Id);

            // throw new Exception("Manually created exeption on delete");

            if (recipe == null) return null;

            _context.Recipes.Remove(recipe);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result) return RequestResult<Unit>.Failure("Error Deleting Recipe");

            return RequestResult<Unit>.Success(Unit.Value);

        }

    }
}
