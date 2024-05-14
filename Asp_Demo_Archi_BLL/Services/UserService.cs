using Asp_Demo_Archi_BLL.Interfaces;
using ASP_Demo_Archi_DAL.Repositories;
using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Demo_Archi_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public User GetUser(int id)
        {
            return _repo.GetUser(id);
        }

        public User Login(string email, string password)
        {
            string verifyPWD = _repo.GetHashPwd(email);
            if(BCrypt.Net.BCrypt.Verify(password, verifyPWD))
            {
                try
                {
                    User connecterUser = _repo.Login(email, verifyPWD);
                    return connecterUser;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new InvalidOperationException("Mot de passe incorrect");
            }
        }

        public void Register(string email, string password, string nickname)
        {
            string hashPWD = BCrypt.Net.BCrypt.HashPassword(password);

            try
            {
                _repo.Register(email, hashPWD, nickname);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
