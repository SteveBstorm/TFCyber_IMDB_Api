using IMDB_Domain.Models;
using Toolbox.RepoTools;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IPersonRepo : IBaseRepository<Person>
    {
        void Create(Person p);
        Person GetById(int id);
    }
}