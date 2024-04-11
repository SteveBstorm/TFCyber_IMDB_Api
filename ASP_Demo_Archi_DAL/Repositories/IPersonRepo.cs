﻿using IMDB_Domain.Models;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IPersonRepo
    {
        void Create(Person p);
        List<Person> GetAll();
        Person GetById(int id);
    }
}