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
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : Controller
    {
        
        private readonly IDbService _dbService;

       
        public ClientsController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpDelete]
        [Route("{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {
            try
            {
                var result = await _dbService.DeleteClient(idClient);
                return Ok(result);
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
