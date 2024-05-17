using Asp_Demo_Archi_BLL.Interfaces;
using IMDB_Api.Models;
using IMDB_Api.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Authorize("isConnectedPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        /// <summary>
        /// Fournit la liste des personnes
        /// </summary>
        /// <returns>Un IEnumerable de Person</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_personService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            return Ok(_personService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]PersonCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            _personService.Create(form.ToDomain());
            return Ok();
        }
    }
}
