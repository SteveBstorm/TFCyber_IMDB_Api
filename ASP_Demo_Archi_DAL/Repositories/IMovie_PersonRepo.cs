using IMDB_Domain.Models;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IMovie_PersonRepo
    {
        void Create(int MovieId, int PersonId, string Role);
        IEnumerable<Actor> GetActors(int movieId);
    }
}