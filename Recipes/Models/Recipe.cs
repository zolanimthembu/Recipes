using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        [Required]
        public string? RecipeName { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public User? User { get; set; }
        [Required]
        public bool isDeleted { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

    }
}
