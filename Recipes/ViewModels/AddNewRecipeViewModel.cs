using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Recipes.ViewModels
{
    public class AddNewRecipeViewModel
    {
        [Required]
        [Display(Name = "Recipe Name")]
        public string? RecipeName { get; set; }
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
