using Asp_Demo_Archi_BLL.Models;
using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Demo_Archi_BLL.Interfaces
{
    public interface IMovieService
    {
        int Create(CompleteMovie movie);
        void Delete(int id);
        void Edit(Movie movie);
        List<Movie> GetAll();
        CompleteMovie GetById(int id);
        List<Movie> GetMovieByPersonId(int PersonId);
    }
}
