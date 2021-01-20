using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos.Authentication;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Services.Interfaces;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.dtos;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;

        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateUserRequest user)
        {
            _userService.Register(user);

            return Ok(new { message = "Registration successful." });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();

            return Ok(result);
        }

    }
}
