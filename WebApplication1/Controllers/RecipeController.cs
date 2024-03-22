using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.ApiModels;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly DbContext _dataContext;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(DbContext context,ILogger<RecipeController> logger)
        {
            _dataContext = context;
            _logger = logger;
        }

[HttpGet("ByName")]
public IActionResult GetRecipeByName(string? recipeName)// Need to change the return type
{
    if (recipeName is null)
        return StatusCode(StatusCodes.Status400BadRequest);
    if (recipeName.Length == 0)
        return StatusCode(StatusCodes.Status204NoContent);

    return Ok(new Recipe { RecipeName = recipeName });// Return 200 with content.
}


        [HttpPost("AddRecipe")]
        public void CreateRecipe(string recipeName)
        {

            var newRecipe = new Recipe()
            {
                RecipeName = recipeName,
                Status = RecipeStatus.InEditing
            };

            var recipeRepository = _dataContext.Set<Recipe>();

            recipeRepository.Add(newRecipe);

            _dataContext.SaveChanges();
        }

        [HttpGet]
        public IList<RecipeModel> GetRecipes()
        {
            return _dataContext.Set<Recipe>()
                .AsEnumerable()
                .Select(x=>RecipeFactory.ConvertToApiModel(x))
                .ToList();
        }
        
        [HttpGet("FullRecipes")]
        public IList<RecipeModel> GetFullRecipes()
        {
            var recipes= _dataContext.Set<Recipe>()
                .Include(x => x.Parameters)
                .OrderBy(x=>x.RecipeName)
                .AsEnumerable()
                .Select(x=>RecipeFactory.ConvertToApiModel(x))
                .ToList();

            return recipes;
        }

        [HttpGet("FullRecipe")]
        public RecipeModel? GetFullRecipe(int recipeId)
        {
            return RecipeFactory.ConvertToApiModel(_dataContext.Set<Recipe>()
                .Include(x => x.Parameters)
                .FirstOrDefault(x=>x.Id== recipeId));
        }

        [HttpPut("Rename/{recipeId}")]
        public IActionResult RenameRecipe(int recipeId, string newName)
        {
            var recipeRepository = _dataContext.Set<Recipe>();

            var recipeToRename = recipeRepository.FirstOrDefault(x => x.Id == recipeId);

            if (recipeToRename == null)
            {
                _logger.LogWarning($"No recipe found with id: {recipeId}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            recipeToRename.RecipeName = newName;

            recipeRepository.Update(recipeToRename);

            _dataContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Duplicate/{recipeId}")]
        public IActionResult DuplicateRecipe(int recipeId, string newRecipeName)
        {
            var recipeRepository = _dataContext.Set<Recipe>();

            var recipeToDuplicate = recipeRepository
                .Include(x => x.Parameters)
                .FirstOrDefault(x => x.Id == recipeId);
            
            if (recipeToDuplicate == null)
            {
                _logger.LogWarning($"No recipe found with id: {recipeId}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var newRecipe = RecipeFactory.DuplicateRecipe(recipeToDuplicate, newRecipeName);

            recipeRepository.Add(newRecipe);
            _dataContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{recipeId}")]
        public void DeleteRecipe(int recipeId)
        {
            _dataContext.Set<Recipe>()
                .Remove(new Recipe { Id = recipeId });

            _dataContext.SaveChanges();

            _logger.LogInformation($"The recipe with id {recipeId} has been deleted!");
        }
    }
}
