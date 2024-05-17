using ASP_Demo_Archi_DAL.Repositories;
using Dapper;
using IMDB_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Demo_Archi_DAL.Services
{
    public class UserService : IUserRepo
    {
        //private string connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private string connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //private readonly string connectionString;

        //public UserService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("default");
        //}

        private readonly SqlConnection connection;
        public UserService(SqlConnection cnx)
        {
            connection = cnx;
        }

        public void Register(string email, string password, string nickname)
        {

            using (SqlCommand cmd = connection.CreateCommand())
            {
                string sql = "INSERT INTO UserAccount (Email, Nickname, Password) " +
                    "VALUES (@email, @nick, @pwd)";
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("nick", nickname);
                cmd.Parameters.AddWithValue("pwd", password);

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { throw ex; }
                connection.Close();
            }

        }

        public User Login(string email, string password)
        {

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Email, Nickname, IsAdmin " +
                    "FROM UserAccount WHERE Email = @email AND Password = @pwd";

                connection.Open();

                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("pwd", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Email = (string)reader["Email"],
                            Id = (int)reader["Id"],
                            Nickname = (string)reader["Nickname"],
                            IsAdmin = (bool)reader["IsAdmin"]
                        };
                    }
                    else throw new InvalidOperationException("Compte utilisateur inexistant");
                }

            }
        }

        public string GetHashPwd(string email)
        {
            string sql = "SELECT Password " +
                    "FROM UserAccount WHERE Email = @email";

            return connection.QueryFirst<string>(sql, email);

            //using (SqlCommand cmd = connection.CreateCommand())
            //{
            //    cmd.CommandText = "SELECT Password " +
            //        "FROM UserAccount WHERE Email = @email";

            //    connection.Open();

            //    cmd.Parameters.AddWithValue("email", email);
            //    string pwd = (string)cmd.ExecuteScalar();
            //    connection.Close();
            //    return pwd;
            //}
        }

    }
}
