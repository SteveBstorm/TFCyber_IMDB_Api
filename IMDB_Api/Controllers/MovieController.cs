using Asp_Demo_Archi_BLL.Interfaces;
using IMDB_Api.Models;
using IMDB_Api.Tools;
using IMDB_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetAll()
        {
            //object truc = new { Nom = "steve", Age = 22, AgeMental = 12 };/

            return Ok(_movieService.GetAll()) ;
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
            return Ok(_movieService.GetById(id));
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MovieCreateForm), StatusCodes.Status400BadRequest)]

        public IActionResult Create(MovieCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            _movieService.Create(form.ToDomain());
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            _movieService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MovieCreateForm), StatusCodes.Status400BadRequest)]

        public IActionResult Update([FromBody]MovieCreateForm m, [FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            Movie movie = m.ToDomain();
            movie.Id = id;
            _movieService.Edit(movie);

            return Ok();

        }

        [Authorize("isConnectedPolicy")]
        [HttpGet("byActorId/{id}")]
        public IActionResult GetByActorId(int id)
        {
            
            return Ok(_movieService.GetMovieByPersonId(id));
        }

    }
}
