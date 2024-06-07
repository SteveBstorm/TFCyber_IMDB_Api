using IMDB_Domain.Models;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IMovieRepo : IBaseRepository<Movie>
    {
        int Create(Movie movie);
        void Delete(int id);
        void Edit(Movie movie);
        Movie GetById(int id);
        List<Movie> GetMovieByPersonId(int PersonId);
    }
}