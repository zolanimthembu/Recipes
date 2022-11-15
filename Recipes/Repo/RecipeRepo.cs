using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Recipes.Data;
using Recipes.Models;
using System.Collections;
using System.Linq;

namespace Recipes.Repo
{
    public class RecipeRepo : IRecipe<Recipe>
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RecipeRepo(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddRecipe(Recipe recipe)
        {
            if(recipe != null)
            { 
                await applicationDbContext.AddAsync(recipe);
                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRecipe(Recipe recipe)
        {
            if (!ReferenceEquals(recipe, null))
            {
                applicationDbContext.Recipe.Update(recipe);
                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable> GetAllRecipes()
        {
            List<Recipe> recipes = await applicationDbContext.Recipe.Where(x => x.isDeleted != true).ToListAsync();
            return recipes;
        }

        public async Task<bool> UpdateRecipe(Recipe recipe)
        {
            if(!ReferenceEquals(recipe, null))
            {
                applicationDbContext.Recipe.Update(recipe);
                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            Recipe recipe = await applicationDbContext.Recipe.Where(x => x.RecipeID == id).AsNoTracking().Include(x=> x.User).FirstOrDefaultAsync();
            if (recipe == null)
            {
                return  null;
            }
            return recipe;
        }
    }
}
