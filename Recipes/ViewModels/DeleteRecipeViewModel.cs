using Recipes.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.ViewModels
{
    public class DeleteRecipeViewModel
    {

        public string? RecipeName { get; set; }
        public string? DateAdded { get; set; }
        public string? UserName { get; set; }
    }
}
