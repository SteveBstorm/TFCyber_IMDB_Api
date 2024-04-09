using ASP_Demo_Archi_DAL.Repositories;
using ASP_Demo_Archi_DAL.Services;
using IMDB_Api.Exemples;
using IMDB_Api.Models;
using IMDB_Api.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepo _movieRepo;

        public MovieController(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //object truc = new { Nom = "steve", Age = 22, AgeMental = 12 };/

            return Ok(_movieRepo.GetAll()) ;
        }

        [HttpPost]
        public IActionResult Create(MovieCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            _movieRepo.Create(form.ToDAL());
            return Ok();
        }
    }
}
