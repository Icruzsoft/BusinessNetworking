using BusinessNetworking.Entities;
using BusinessNetworking.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessNetworking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NetworkingController : ControllerBase
    {        
        private readonly IUserService _userService;

        public NetworkingController(IUserService userService)
        {            
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var registeredUser = await _userService.RegisterUser(user);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var token = await _userService.AuthenticateUser(email, password);

                if (token == null)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}