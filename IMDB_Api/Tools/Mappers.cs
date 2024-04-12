using IMDB_Domain.Models;
using IMDB_Api.Models;

namespace IMDB_Api.Tools
{
    public static class Mappers
    {
        public static Movie ToDomain(this MovieCreateForm form)
        {
            return new Movie
            {
                Title = form.Title,
                Description = form.Description,
                RealisatorId = form.RealisatorId
            };
        }

        public static Person ToDomain(this PersonCreateForm form)
        {
            return new Person { 
                Firstname = form.Firstname,
                Lastname = form.Lastname,
                PictureURL = form.PictureUrl
            };
        }
    }
}
