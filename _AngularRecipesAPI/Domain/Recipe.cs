namespace Domain;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Servings { get; set; }
    public decimal? Cost { get; set; }
    public string? Origin { get; set; }
    public string Instructions { get; set; }

}
