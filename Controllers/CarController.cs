using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ICloudinaryService _cloudinaryService;

        public CarController(ICarService carService, ICloudinaryService cloudinaryService)
        {
            _carService = carService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet("GetCars")]
        public IActionResult GetCars(int pageNumber = 1, int pageQuantity = 10)
        {
            try
            {
                var cars = _carService.GetCars(pageNumber, pageQuantity);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}", Name = "GetCarById")]
        public ActionResult<Car> GetCarById(Guid id)
        {
            try
            {
                var produto = _carService.GetCarById(id);
                if (produto == null)
                {
                    return NotFound(new { message = "Produto not found." });
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("PostCar")]
        public IActionResult PostCar([FromForm] CreateCarDTO dto)
        {
            try
            {
                var car = _carService.PostCarCloudinary(dto);
                return CreatedAtRoute(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}", Name = "UpdateCar")]
        public IActionResult UpdateCar(Guid id, [FromForm] UpdateCarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _carService.UpdateCar(id, dto);
                return Ok(new { message = "Product updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}", Name = "RemoveCar")]
        public IActionResult DeleteCar(Guid id)
        {
            try
            {
                _carService.DeleteCar(id);
                return Ok(new { message = "Product successfully deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Filter")]
        public IActionResult FilterCars([FromQuery] CarFilterDTO filter)
        {
            try
            {
                var result = _carService.FilterCars(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
