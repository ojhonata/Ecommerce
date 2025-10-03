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
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetCars")]
        public IActionResult GetCars()
        {
            try 
            {
                var cars = _carService.GetCars();
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
                    return NotFound(new { message = "Produto não encontrado." });
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("PostCar")]
        public ActionResult<Car> PostCar([FromForm] CreateCarDTO carDto)
        {
            try
            {
                var car = _carService.PostCar(carDto);
                return CreatedAtRoute(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}", Name = "UpdateCar")]
        public IActionResult UpdateCar(Guid id, [FromBody] Car car)
        {
            if (id != car.Id)
            {
                return BadRequest(new { message = "ID do car inválido." });
            }
            try
            {
                _carService.UpdateProduto(car);
                return Ok(new { message = "Produto atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}", Name = "DeleteCar")]
        public IActionResult DeleteCar(Guid id)
        {
            try
            {
                _carService.DeleteCar(id);
                return Ok(new { message = "Produto deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}