using ASP_Demo_Archi_DAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Demo_Archi_DAL.Services
{
    public class Movie_PersonService : IMovie_PersonRepo
    {
        //private string connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private string connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private string connectionString;
        public Movie_PersonService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }
        public void Create(int MovieId, int PersonId, string Role)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Movie_Person (MovieId, PersonId, Role) " +
                        "VALUES (@mId, @pId, @role)";

                    cmd.Parameters.AddWithValue("mId", MovieId);
                    cmd.Parameters.AddWithValue("pId", PersonId);
                    cmd.Parameters.AddWithValue("role", Role);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}
