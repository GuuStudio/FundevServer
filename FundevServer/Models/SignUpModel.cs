using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FundevServer.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = String.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
