using ASP_Demo_Archi_DAL.Repositories;
using IMDB_Api.Models;
using IMDB_Api.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepo _personRepo;
        public PersonController(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }
        /// <summary>
        /// Fournit la liste des personnes
        /// </summary>
        /// <returns>Un IEnumerable de Person</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_personRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            return Ok(_personRepo.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]PersonCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            _personRepo.Create(form.ToDal());
            return Ok();
        }
    }
}
