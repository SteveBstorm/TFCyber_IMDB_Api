using ASP_Demo_Archi_DAL.Repositories;
using Dapper;
using IMDB_Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Services
{
    public class CommentService : BaseRepository<Comment>, ICommentRepo
    {
        private SqlConnection _connection;
        public CommentService(SqlConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public IEnumerable<Comment> GetByMovieId(int movieId)
        {
            return _connection.Query<Comment>("SELECT * FROM Comment WHERE MovieId = @movieId", new { movieId });
        }
    }
}
