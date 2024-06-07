using IMDB_Domain.Models;
using ASP_Demo_Archi_DAL.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Services
{
    public class MovieService : BaseRepository<Movie>, IMovieRepo
    {
        //private string connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=TFCyberSecu_MovieDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private string connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private string connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //public List<Movie> maListe { get; set; }

        private string connectionString;
        public MovieService(SqlConnection connection, IConfiguration config) : base(connection)
        {
            connectionString = config.GetConnectionString("default");
        }

        protected override Movie Converter(SqlDataReader reader)
        {
            return new Movie
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
                RealisatorId = (int)reader["RealisatorId"]
            };
        }

        //public List<Movie> GetAll()
        //{
        //    string sql = "SELECT * FROM Movie";
        //    SqlConnection cnx = new SqlConnection(connectionString);
        //    return cnx.Query<Movie>(sql).ToList();

        //    //List<Movie> list = new List<Movie>();
        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    using (SqlCommand command = connection.CreateCommand())
        //    //    {
        //    //        command.CommandText = "SELECT * FROM Movie";
        //    //        connection.Open();
        //    //        using (SqlDataReader reader = command.ExecuteReader())
        //    //        {
        //    //            while (reader.Read())
        //    //            {
        //    //                list.Add(Converter(reader));
        //    //            }
        //    //        }
        //    //        connection.Close();
        //    //    }
        //    //}
        //    //return list;
        //}

        public Movie GetById(int id)
        {
            Movie m = new Movie();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Movie WHERE Id = @id";
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            m = Converter(reader);
                        }
                    }
                    connection.Close();
                }
            }
            return m;
        }

        public int Create(Movie movie)
        {
            CheckMovieExists();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Movie (Title, Description, RealisatorId) " +
                        "OUTPUT inserted.Id " +
                        "VALUES (@title, @desc, @real)";

                    cmd.Parameters.AddWithValue("title", movie.Title);
                    cmd.Parameters.AddWithValue("desc", movie.Description);
                    cmd.Parameters.AddWithValue("real", movie.RealisatorId);

                    try
                    {
                        connection.Open();
                        int createdId = (int)cmd.ExecuteScalar();
                        connection.Close();
                        return createdId;
                    }
                    catch (SqlException ex)
                    {
                        //Gérer l'exception
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            //movie.Id = (maListe.Count() > 0) ? maListe.Max(s => s.Id) + 1 : 1;
            //maListe.Add(movie);
        }

        public void Edit(Movie movie)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Movie SET Title = @title, Description = @desc, RealisatorId = @real " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", movie.Id);
                    cmd.Parameters.AddWithValue("title", movie.Title);
                    cmd.Parameters.AddWithValue("desc", movie.Description);
                    cmd.Parameters.AddWithValue("real", movie.RealisatorId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //int index = maListe.FindIndex(f => f.Id == movie.Id);
            //maListe[index] = movie;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM Movie WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", id);


                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Movie aSupprimer = maListe.SingleOrDefault(f => f.Id == id);
            //maListe.Remove(aSupprimer);
        }

        public List<Movie> GetMovieByPersonId(int PersonId)
        {
            List<Movie> list = new List<Movie>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Movie m JOIN Movie_Person mp " +
                        "ON m.Id = mp.MovieId " +
                        "WHERE mp.PersonId = @id";
                    command.Parameters.AddWithValue("id", PersonId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Converter(reader));
                        }
                    }
                    connection.Close();
                }
            }
            return list;
        }

        public void CheckMovieExists()
        {

        }
    }
}
