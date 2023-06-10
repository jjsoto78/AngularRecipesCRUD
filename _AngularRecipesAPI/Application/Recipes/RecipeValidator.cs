using Domain;
using FluentValidation;

namespace Application.Recipes;

public class RecipeValidator : AbstractValidator<Recipe>
{
    const int MIN_LENGTH_NAME = 5;
    const int MAX_LENGTH_NAME = 25;
    const int MIN_LENGTH_INSTRUCTIONS = 10;
    const int MAX_LENGTH_INSTRUCTIONS = 150;
    public RecipeValidator()
    {
         RuleFor(y => y.Name).NotEmpty().MinimumLength(MIN_LENGTH_NAME).MaximumLength(MAX_LENGTH_NAME);
         RuleFor(y => y.Instructions).NotEmpty().MinimumLength(MIN_LENGTH_INSTRUCTIONS).MaximumLength(MAX_LENGTH_INSTRUCTIONS);
    }
}
