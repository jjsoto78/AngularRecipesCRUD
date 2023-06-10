using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Recipes;

public class ListRecipes
{
    public class Query : IRequest<RequestResult<List<Recipe>>>{}

    public class Handler : IRequestHandler<Query, RequestResult<List<Recipe>>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context )
        {
            _context = context;
        }

        public async Task<RequestResult<List<Recipe>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recipes = await _context.Recipes.ToListAsync();

            return RequestResult<List<Recipe>>.Success(recipes);
        }
    }

}
