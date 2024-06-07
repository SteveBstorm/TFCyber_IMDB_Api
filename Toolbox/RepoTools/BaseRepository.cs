using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Toolbox.RepoTools
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public SqlConnection Connection { get; set; }
        public BaseRepository(SqlConnection connection)
        {
            Connection = connection;
        }

        protected abstract TEntity Converter(SqlDataReader reader);


        public virtual IEnumerable<TEntity> GetAll(string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = (typeof(TEntity)).Name;
            }

            string sqlQuery = $"SELECT * FROM {tableName}";


            using (SqlCommand cmd = Connection.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return Converter(reader);
                    }
                }
            }
        }
    }
}
