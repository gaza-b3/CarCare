using Shared;

namespace WebApplication1.Domain
{
    public class Recipe
    {
        public int Id { get; set; }

        public string RecipeName { get; set; }

        public RecipeStatus Status { get; set; }

        public IList<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
