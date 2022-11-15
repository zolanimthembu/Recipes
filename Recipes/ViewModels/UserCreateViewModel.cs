using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.ViewModels
{
    public class UserCreateViewModel
    {
        public string? AspUserID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm passowrd")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
