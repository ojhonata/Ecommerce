using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var user = _userService.GetByEmail(dto.Email);

                if (user == null || user.Password != dto.Password)
                {
                    return Unauthorized(new { message = "Email ou senha inválidos." });
                }

                var token = TokenService.GenerateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            ;
        }
    }
}
