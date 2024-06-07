using IMDB_Domain.Models;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IUserRepo : IBaseRepository<User>
    {
        User Login(string email, string password);
        void Register(string email, string password, string nickname);
        string GetHashPwd(string email);
        User GetUser(int id);
    }
}