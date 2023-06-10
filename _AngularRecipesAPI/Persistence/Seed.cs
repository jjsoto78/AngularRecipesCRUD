namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Recipes.Any()) return;

        var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Honey Glazed Salmon",
                    Servings = 1,
                    Cost = 10.0M,
                    Origin = "Norway",
                    Instructions = "1.Cook Salmon in olive oil for 10 min 2. Pour Honey over and cook again for 1 min 3. Add a pich of salt",
                },
                new Recipe
                {
                    Name = "Pepperoni Pineapple Pizza",
                    Servings = 4,
                    Cost = 20.0M,
                    Origin = "Latam",
                    Instructions = "1. Add lots of cheese to dough 2. Add tomato paste and Peperoni and Pineapple cook in oven for 10 min",
                },
               
            };

        await context.Recipes.AddRangeAsync(recipes);
        await context.SaveChangesAsync();
    }
}
