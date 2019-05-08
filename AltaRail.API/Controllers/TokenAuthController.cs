using AltaRail.Application.DTOs;
using AltaRail.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace AltaRail.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenAuthController(IConfiguration config, IUserService userService, ITokenService tokenService)
        {
            _config = config;
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            try
            {
                var isAuthenticated = await _userService.Authenticate(login.Username, login.Password);
                if (!isAuthenticated)
                    return Unauthorized(new { message = "Invalid Credentials" });

                var tokenDto = new CreateTokenDto
                {
                    Issuer = _config["Jwt:Issuer"],
                    Key = _config["Jwt:Key"]
                };

                var token = _tokenService.CreateToken(tokenDto);
                return Json(token);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
