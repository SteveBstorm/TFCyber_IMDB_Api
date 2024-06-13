using IMDB_Domain.Models;
using IMDB_Api.Models;
using Asp_Demo_Archi_BLL.Models;

namespace IMDB_Api.Tools
{
    public static class Mappers
    {
        

        public static CompleteMovie ToDomain(this MovieCreateForm form)
        {
            return new CompleteMovie
            {
                Title = form.Title,
                Description = form.Description,
                RealisatorId = form.RealisatorId,
                Casting = form.Casting
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
