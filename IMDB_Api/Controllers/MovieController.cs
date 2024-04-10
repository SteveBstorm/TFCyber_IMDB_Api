using ASP_Demo_Archi_DAL.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetAll()
        {
            //object truc = new { Nom = "steve", Age = 22, AgeMental = 12 };/

            return Ok(_movieRepo.GetAll()) ;
        }
        /// <summary>
        /// Faut pas respirer la compote, ça fait tousser
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objet de type Movie</returns>
        /// <remarks>Cette méthode prend un Id de film en paramètre et 
        /// retourne un Ok contenant l'objet</remarks>
        /// <response code="200">C'est cool ça marche</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetById(int id)
        {
            return Ok(_movieRepo.GetById(id));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MovieCreateForm), StatusCodes.Status400BadRequest)]

        public IActionResult Create(MovieCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            _movieRepo.Create(form.ToDAL());
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            _movieRepo.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MovieCreateForm), StatusCodes.Status400BadRequest)]

        public IActionResult Update([FromBody]MovieCreateForm m, [FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            Movie movie = m.ToDAL();
            movie.Id = id;
            _movieRepo.Edit(movie);

            return Ok();

        }

        [HttpGet("byActorId/{id}")]
        public IActionResult GetByActorId(int id)
        {
            return Ok(_movieRepo.GetMovieByPersonId(id));
        }

    }
}
