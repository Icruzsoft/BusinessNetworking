using BusinessNetworking.Entities;
using BusinessNetworking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;
using System.Threading.Tasks;

namespace BusinessNetworking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAll")] // Habilitar CORS para este controlador
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Asegurarse de que CreatedDate se establezca aquí ya que no se envía desde el cliente
            user.CreatedDate = DateTime.UtcNow;

            try
            {
                // Pasar el objeto user directamente al método RegisterUser
                var registeredUser = await _userService.RegisterUser(user);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                // Es recomendable mejorar el manejo de excepciones para diferenciar entre errores de validación y otros tipos de errores
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await _userService.AuthenticateUser(loginModel.Email, loginModel.Password);

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

    // Puedes necesitar definir un modelo para el proceso de login si no lo has hecho ya
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
