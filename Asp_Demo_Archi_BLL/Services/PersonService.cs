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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepo _personRepo;

        public PersonService(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }

        public void Create(Person p)
        {
            _personRepo.Create(p);
        }

        public List<Person> GetAll()
        {
            return _personRepo.GetAll();
        }

        public Person GetById(int id)
        {
            return _personRepo.GetById(id);
        }
    }
}
