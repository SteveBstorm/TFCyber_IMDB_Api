using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.RepoTools
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public SqlConnection Connection { get; set; }
        public BaseRepository(SqlConnection connection)
        {
            Connection = connection;
        }

        //protected abstract TEntity Converter(SqlDataReader reader);

        protected virtual TEntity Converter(SqlDataReader reader)
        {
            TEntity entity = new TEntity();
            PropertyInfo[] props = typeof(TEntity).GetProperties();

            foreach (PropertyInfo prop in props) 
            {
                prop.SetValue(entity, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
            }
            return entity;
        }

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

        /*
            Ajouter une méthode générique : GetById(int id)
            Ajouter une méthode générique : Delete(int id)

            Rendre le tout fonctionnel 

            Bonus : Essayer de mettre en place un mapper/converter générique
            
         */
        public virtual TEntity GetById(int id, string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = (typeof(TEntity)).Name;
            }

            string sqlQuery = $"SELECT * FROM {tableName} WHERE Id = @id";


            using (SqlCommand cmd = Connection.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("id", id);
                Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return Converter(reader);
                    }
                }
                Connection.Close();
            }
            return null;
        }

        public virtual void Delete(int id, string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = (typeof(TEntity)).Name;
            }

            string sqlQuery = $"DELETE FROM {tableName} WHERE Id = @id";


            using (SqlCommand cmd = Connection.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("id", id);
                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }

        }

        public virtual int Create(TEntity entity, string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = (typeof(TEntity)).Name;
            }

            PropertyInfo[] props = typeof(TEntity).GetProperties().Where(p => p.Name != "Id").ToArray();

            string sql = $"INSERT INTO {tableName} " +
                $"(";
            
            string columnName = string.Join(",", props.Select(p => p.Name));

            sql += columnName;
            sql += ") OUTPUT inserted.Id VALUES (";

            string values = string.Join(",", props.Select(p => "@"+p.Name));
            sql += values + ")";

            using(SqlCommand cmd = Connection.CreateCommand()) 
            {
                cmd.CommandText = sql;
                foreach(PropertyInfo prop in props)
                {
                    cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(entity));
                }
                try
                {
                    Connection.Open();
                    int insertedId = (int)cmd.ExecuteScalar();
                    Connection.Close();
                    return insertedId;
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public virtual void Edit(TEntity entity, string tableName = "")
        {
            if(entity is null) { throw new ArgumentNullException(nameof(entity)); }

            if (string.IsNullOrEmpty(tableName))
            {
                tableName = (typeof(TEntity)).Name;
            }

            PropertyInfo[] props = typeof(TEntity).GetProperties().Where(p => p.Name != "Id").ToArray();

            string sql = $"UPDATE {tableName} SET ";

            foreach(PropertyInfo prop in props)
            {
                sql += prop.Name + " = @" + prop.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE Id = @Id";

            using(SqlCommand cmd = Connection.CreateCommand())
            {
                cmd.CommandText = sql;

                foreach(PropertyInfo prop in typeof(TEntity).GetProperties()) 
                {
                    cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(prop.Name));
                }

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();

            }
                
        }
    }
}
