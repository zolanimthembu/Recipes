using System.ComponentModel.DataAnnotations;

namespace Recipes.ViewModels
{
    public class AdminCreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role Name")]
        public string? RoleName { get; set; }
    }
}
