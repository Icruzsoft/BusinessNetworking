using Microsoft.AspNetCore.Mvc;

namespace BusinessNetworking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NetworkingController : ControllerBase
    {        
        private readonly UserService _userService;

        public NetworkingController(UserService userService)
        {            
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel model)
        {
            try
            {
                var registeredUser = await _userService.RegisterUser(model);
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