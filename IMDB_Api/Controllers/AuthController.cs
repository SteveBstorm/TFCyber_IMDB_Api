using Asp_Demo_Archi_BLL.Interfaces;
using IMDB_Api.Models;
using IMDB_Api.Tools;
using IMDB_Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(IUserService userService, TokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;

        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginFrom loginInfo)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                User connectedUser = _userService.Login(loginInfo.Email, loginInfo.Password);
                string token = _tokenGenerator.GenerateToken(connectedUser);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterForm form) 
        { 
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                _userService.Register(form.Email, form.Password, form.Nickname);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
