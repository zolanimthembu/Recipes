using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "bit")]
        public bool isDeleted { get; set; }
    }
}
