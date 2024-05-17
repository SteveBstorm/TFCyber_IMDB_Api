using IMDB_Domain.Models;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IUserRepo
    {
        User Login(string email, string password);
        void Register(string email, string password, string nickname);
        string GetHashPwd(string email);
        User GetUser(int id);
    }
}