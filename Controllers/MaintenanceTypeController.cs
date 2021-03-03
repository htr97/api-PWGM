using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    public class MaintenanceTypeController : BaseApiController
    {
        private readonly DataContext _context;
        public MaintenanceTypeController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceType>>> GetMaintenanceTypes(){
            return await _context.MaintenanceTypes.ToListAsync();   
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostMainType(MaintenanceType maintenance)
        {
            _context.MaintenanceTypes.Add(maintenance);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenanceType(int id, MaintenanceType maintenance)
        {
            
            if(id != maintenance.Id){
                return BadRequest();
            }

            _context.Entry(maintenance).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceType(int id){
            
            var maintenanceType = _context.MaintenanceTypes.FirstOrDefault(m => m.Id == id);

            if (maintenanceType == null){
                return NotFound();
            }

            _context.MaintenanceTypes.Remove(maintenanceType);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}