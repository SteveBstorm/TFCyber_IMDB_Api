using IMDB_Domain.Models;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IMovieRepo : IBaseRepository<Movie>
    {
        
        List<Movie> GetMovieByPersonId(int PersonId);
    }
}