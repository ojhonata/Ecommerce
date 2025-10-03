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
        public IActionResult PostUsuario([FromBody] UserDTO userDTO)
        {
            try
            {
                User user = _userService.PostUser(userDTO);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}