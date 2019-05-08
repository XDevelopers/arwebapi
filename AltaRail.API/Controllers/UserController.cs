using AltaRail.Application.DTOs;
using AltaRail.Application.Exceptions;
using AltaRail.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltaRail.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserDto userDto)
        {
            try
            {
                await _userService.CreateUser(userDto);
                return Ok();
            }
            catch (UserExistsException)
            {
                return BadRequest(new { message = "Ivalid user" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Json(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string id)
        {
            var user = await _userService.GetUser(id);
            return Json(user);
        }

        [HttpPut("edit"), Authorize]
        public async Task<ActionResult> EditUser([FromBody] EditUserDto editUser)
        {
            await _userService.EditUser(editUser);
            return Ok();
        }
    }
}
