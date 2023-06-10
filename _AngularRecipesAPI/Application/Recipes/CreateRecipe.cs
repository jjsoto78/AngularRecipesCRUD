using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using Application.Core;


namespace Application.Recipes;

public class CreateRecipe
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

        public Handler(DataContext context )
        {
            _context = context;
        }

        public async Task<RequestResult<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Recipes.Add(request.Recipe);
            
            var result = (await _context.SaveChangesAsync() > 0);

            if(!result) return RequestResult<Unit>.Failure("Error Creating Activity");

            return RequestResult<Unit>.Success(Unit.Value);
        }
    }
}
