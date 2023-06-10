using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Recipes;

public class RecipeDetails
{
    public class Query : IRequest<RequestResult<Recipe>>{
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, RequestResult<Recipe>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context )
        {
            _context = context;
        }

        public async Task<RequestResult<Recipe>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes.FindAsync(request.Id);

            return RequestResult<Recipe>.Success(recipe);
        }
    }

}
