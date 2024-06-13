using Asp_Demo_Archi_BLL.Interfaces;
using Asp_Demo_Archi_BLL.Models;
using ASP_Demo_Archi_DAL.Repositories;
using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Asp_Demo_Archi_BLL.Services { 
    public class MovieService : IMovieService
    {

        private readonly IMovieRepo _movieRepo;
        private readonly IPersonRepo _personRepo;
        private readonly IMovie_PersonRepo _mpRepo;
        public MovieService(IMovieRepo movieRepo, IPersonRepo personRepo, IMovie_PersonRepo mpRepo)
        {
            _movieRepo = movieRepo;
            _personRepo = personRepo;
            _mpRepo = mpRepo;
        }
        public int Create(CompleteMovie movie)
        {

            int newMovieId = _movieRepo.Create(movie);
            foreach(Actor a in movie.Casting)
            {
                _mpRepo.Create(newMovieId, a.Id, a.Role);
            }
            return newMovieId;

        }

        public void Delete(int id)
        {
            _movieRepo.Delete(id);
        }

        public void Edit(Movie movie)
        {
            _movieRepo.Edit(movie);
        }

        public List<Movie> GetAll()
        {
            return _movieRepo.GetAll().ToList();
        }

        public CompleteMovie GetById(int id)
        {
            Movie m = _movieRepo.GetById(id);
            CompleteMovie result = new CompleteMovie();
            result.Id = m.Id;
            result.Title = m.Title;
            result.Description = m.Description;
            result.Realisator = _personRepo.GetById(m.RealisatorId);
            result.Casting = _mpRepo.GetActors(m.Id);

            return result;
        }

        public List<Movie> GetMovieByPersonId(int PersonId)
        {
            return _movieRepo.GetMovieByPersonId(PersonId);
        }
    }
}
