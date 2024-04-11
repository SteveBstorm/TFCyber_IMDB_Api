﻿using IMDB_Domain.Models;

namespace ASP_Demo_Archi_DAL.Repositories
{
    public interface IMovieRepo
    {
        int Create(Movie movie);
        void Delete(int id);
        void Edit(Movie movie);
        List<Movie> GetAll();
        Movie GetById(int id);
        List<Movie> GetMovieByPersonId(int PersonId);
    }
}