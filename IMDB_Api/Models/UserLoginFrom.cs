using System.ComponentModel.DataAnnotations;

namespace IMDB_Api.Models
{
    public class UserLoginFrom
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir les trucs de base ")]
        public string Password { get; set; }
    }
}
