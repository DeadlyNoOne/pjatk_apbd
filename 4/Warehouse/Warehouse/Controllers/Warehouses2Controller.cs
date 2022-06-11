using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Warehouse.Models;
using Warehouse.Services;

namespace Warehouse.Controllers
{
    [Route("api/warehouses2")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IDbServiceWithProc _dbService;

        public Warehouses2Controller(IDbServiceWithProc dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] ProductWarehouse productWarehouse)
        {
            int idProductWarehouse;
            try { idProductWarehouse = await _dbService.addToWarehouseAsync(productWarehouse); }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok($"Succsesfully added! ID: {idProductWarehouse}!");
        }
    }
}

