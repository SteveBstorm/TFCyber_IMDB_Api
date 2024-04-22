﻿using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Demo_Archi_BLL.Interfaces
{
    public interface IPersonService
    {
        void Create(Person p);
        List<Person> GetAll();
        Person GetById(int id);
    }
}
