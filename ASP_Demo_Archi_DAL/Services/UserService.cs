using ASP_Demo_Archi_DAL.Models;
using ASP_Demo_Archi_DAL.Repositories;
using Microsoft.Data.SqlClient;
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
        private string connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TFCyber_IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void Register(string email, string password, string nickname)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO UserAccount (Email, Nickname, Password) " +
                        "VALUES (@email, @nick, @pwd)";
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("nick", nickname);
                    cmd.Parameters.AddWithValue("pwd", password);

                    cnx.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex) { throw ex; }
                    cnx.Close();
                }
            }
        }

        public User Login(string email, string password)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Email, Nickname, IsAdmin " +
                        "FROM UserAccount WHERE Email = @email AND Password = @pwd";

                    cnx.Open();

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
        }

        public string GetHashPwd(string email)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT Password " +
                        "FROM UserAccount WHERE Email = @email";

                    cnx.Open();

                    cmd.Parameters.AddWithValue("email", email);
                    string pwd = (string)cmd.ExecuteScalar();
                    cnx.Close();
                    return pwd;
                }
            }
        }
    }
}
