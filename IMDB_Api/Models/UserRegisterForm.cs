using System.ComponentModel.DataAnnotations;

namespace IMDB_Api.Models
{
    public class UserRegisterForm
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir les trucs de base ")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage ="Les deux mots de passe doivent correspondre")]
        public string PasswordConfirm { get; set; }
    }
}
