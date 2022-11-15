using System.ComponentModel.DataAnnotations;

namespace Recipes.ViewModels
{
    public class GetAllRecipeViewModel
    {
        public int RecipeID { get; set; }
        [Required]
        [Display(Name ="Recipe Name")]
        public string? RecipeName { get; set; }
    }
}
