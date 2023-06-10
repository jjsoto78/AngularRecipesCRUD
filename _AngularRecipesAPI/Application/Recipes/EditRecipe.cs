using Domain;
using Persistence;
using MediatR;
using AutoMapper;
using FluentValidation;
using Application.Core;


namespace Application.Recipes;

public class EditRecipe
{
    public class Command : IRequest<RequestResult<Unit>>
    {
        public Recipe Recipe { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(y => y.Recipe).SetValidator(new RecipeValidator());
        }
    }

    public class Handler : IRequestHandler<Command, RequestResult<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<RequestResult<Unit>> IRequestHandler<Command, RequestResult<Unit>>.Handle(Command request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes.FindAsync(request.Recipe.Id);

            if(recipe == null) return null;

            _mapper.Map(request.Recipe, recipe);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result) return RequestResult<Unit>.Failure("Error Updating Recipe");

            return RequestResult<Unit>.Success(Unit.Value);
        }
    }

}
