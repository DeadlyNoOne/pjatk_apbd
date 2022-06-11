using Cw4.Models;
using Cw4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IFileDbService _fileDbService;
        public AnimalsController(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }



        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            var Animals = _fileDbService.GetAnimals(orderBy);
            return Ok(Animals);
        }



        [HttpPost]
        public IActionResult AddAnimal(Animal Animal)
        {
            Animal animal = _fileDbService.AddAnimal(Animal);

            return Created("", Animal);


        }

        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(Animal animal, string idAnimal)
        {
            int result = _fileDbService.UpdateAnimal(animal, idAnimal);
            if (result == 0)
            {
                return Ok();
            }
            else if (result == 1)
            {
                return NotFound("animal o podanym numerze nie istnieje");
            }
            else
                return BadRequest();
        }

        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(string idAnimal)
        {
            int result = _fileDbService.DeleteAnimal(idAnimal);
            if (result == 0)
            {
                return Ok(idAnimal);
            }
            else if (result == 1)
            {
                return NotFound("Animal o podanym numerze nie istnieje");
            }
            else
                return BadRequest();
        }

    }
}
