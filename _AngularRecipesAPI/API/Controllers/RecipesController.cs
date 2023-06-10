using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Recipes;

namespace API.Controllers;

public class RecipesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetRecipes()
    {
    //    throw new Exception("Manually created exeption in GetRecipes controller");

        var result = await Mediator.Send(new ListRecipes.Query());

        return HandleRequestResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(Guid id)
    {
        var result = await Mediator.Send(new RecipeDetails.Query { Id = id });

        return HandleRequestResult(result);

    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
    {
        return HandleRequestResult(await Mediator.Send(new CreateRecipe.Command { Recipe = recipe }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] Recipe recipe)
    {
        recipe.Id = id;
        return HandleRequestResult(await Mediator.Send(new EditRecipe.Command { Recipe = recipe }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        return HandleRequestResult(await Mediator.Send(new DeleteRecipe.Command { Id = id }));
    }
}
