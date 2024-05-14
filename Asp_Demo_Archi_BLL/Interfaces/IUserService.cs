using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Demo_Archi_BLL.Interfaces
{
    public interface IUserService
    {
        User Login(string email, string password);
        void Register(string email, string password, string nickname);
        User GetUser(int id);
    }
}
