using Recipes.Models;
using System.Collections;

namespace Recipes.Repo
{
    public interface IRecipe<Recipe>
    {
        Task<bool> AddRecipe(Recipe recipe);
        Task<Recipe> GetRecipeById(int id);
        Task<IEnumerable> GetAllRecipes();
        Task<bool> UpdateRecipe(Recipe recipe);
        Task<bool> DeleteRecipe(Recipe recipe);

    }
}
