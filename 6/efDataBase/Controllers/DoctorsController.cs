using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using efDataBase.Models;
using efDataBase.Services;
using efDataBase.Models.DTO;

namespace efDataBase.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorsController : Controller
    {
        
        private readonly IDbService _dbService;

       
        public DoctorsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _dbService.GetDoctors();
            return Ok(doctors);
        }

        [HttpDelete]
        [Route("{idDoctor}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idDoctor)
        {
            try
            {
                var result = await _dbService.DeleteDoctor(idDoctor);
                return Ok(result);
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        [Route("{idDoctor}")]
        public async Task<IActionResult> EditDoctor([FromBody] SomeSortOfDoctor doctor, [FromRoute] int idDoctor)
        {
            try
            {
                var result = await _dbService.EditDoctor(doctor, idDoctor);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] SomeSortOfDoctor doctor)
        {
            try
            {
                var result = await _dbService.AddDoctor(doctor);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
