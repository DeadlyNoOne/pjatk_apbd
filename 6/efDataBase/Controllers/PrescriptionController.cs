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
    [Route("api/prescription")]
    [ApiController]
    public class PrescriptionController : Controller
    {
        
        private readonly IDbPrescriptionService _dbService;

       
        public PrescriptionController(IDbPrescriptionService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("{idPrescription}")]
        public async Task<IActionResult> GetPrescription([FromRoute] int idPrescription)
        {
            var doctors = await _dbService.GetPrescription(idPrescription);
            return Ok(doctors);
        }

        

    }
}
