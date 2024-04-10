using System.ComponentModel.DataAnnotations;

namespace IMDB_Api.Models
{
    public class PersonCreateForm
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string PictureUrl { get; set; }
    }
}
