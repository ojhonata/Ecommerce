using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUser()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("PostUser")]
        public IActionResult PostUsuario([FromBody] CreateUserDto user)
        {
            try
            {
                User users = _userService.PostUser(user);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            try
            {
                var user = _userService.GetByEmail(email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{email}")]
        public IActionResult UpdateUser(string email, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                _userService.UpdateUser(email, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
