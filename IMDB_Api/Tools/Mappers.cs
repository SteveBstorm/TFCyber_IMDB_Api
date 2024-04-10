using ASP_Demo_Archi_DAL.Models;
using IMDB_Api.Models;

namespace IMDB_Api.Tools
{
    public static class Mappers
    {
        public static Movie ToDAL(this MovieCreateForm form)
        {
            return new Movie
            {
                Title = form.Title,
                Description = form.Description,
                RealisatorId = form.RealisatorId
            };
        }

        public static Person ToDal(this PersonCreateForm form)
        {
            return new Person { 
                Firstname = form.Firstname,
                Lastname = form.Lastname,
                PictureURL = form.PictureUrl
            };
        }
    }
}
