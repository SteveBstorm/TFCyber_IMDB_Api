using ASP_Demo_Archi_DAL.Repositories;
using IMDB_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Demo_Archi_DAL.Services
{
    public class PersonService : IPersonRepo
    {
        //private string connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private string connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private string connectionString;
        public PersonService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }
        private Person Converter(SqlDataReader reader)
        {
            return new Person
            {
                Id = (int)reader["Id"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
                PictureURL = (string)reader["PictureURL"]
            };
        }

        public List<Person> GetAll()
        {
            List<Person> list = new List<Person>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Person";
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

        public void Create(Person p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Person (Firstname, Lastname, PictureURL) " +
                        "VALUES (@fn, @ln, @url)";

                    cmd.Parameters.AddWithValue("fn", p.Firstname);
                    cmd.Parameters.AddWithValue("ln", p.Lastname);
                    cmd.Parameters.AddWithValue("url", p.PictureURL);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }

        public Person GetById(int id)
        {
            Person m = new Person();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Person WHERE Id = @id";
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
    }

}
