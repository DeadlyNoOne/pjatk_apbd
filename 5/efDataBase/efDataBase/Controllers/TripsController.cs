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
    [Route("api/trips")]
    [ApiController]
    public class TripsController : Controller
    {
        
        private readonly IDbService _dbService;

       
        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }


        [HttpPost]
        [Route("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip([FromBody] SomeSortOfClientThatWantATrip client, [FromRoute] int idTrip)
        {
            try
            {
                var result = await _dbService.AddClientToTrip(client, idTrip);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
