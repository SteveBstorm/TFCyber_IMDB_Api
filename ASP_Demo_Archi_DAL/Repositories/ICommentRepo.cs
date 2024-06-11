using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface ICommentRepo : IBaseRepository<Comment>
    {
        IEnumerable<Comment> GetByMovieId(int movieId);
    }
}
