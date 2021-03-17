using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;

namespace Controllers
{
    public class MaintenanceController : BaseApiController
    {
        private readonly DataContext _context;
        public MaintenanceController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<GetMaintenanceDto>> GetMaintenance()
        {
             return await (from m in _context.Maintenances
                join e in _context.Equipments on m.EquipmentId equals e.Id
                join u in _context.Ubications on e.UbicationId equals u.Id
                join c in _context.Companies on u.CompanyId equals c.Id
                join ur in _context.Users on new {k1 = c.Id, k2 = m.User.Id} equals new {k1 = ur.CompanyId, k2 = ur.Id}
                join p in _context.Priorities on m.PriorityId equals p.Id
                join pr in _context.Problems on m.ProblemId equals pr.Id
                join mt in _context.MaintenanceTypes on m.MaintenanceTypeId equals mt.Id
                select new GetMaintenanceDto {DeviceName = e.DeviceName, StartDate = m.StartDate, EndDate = m.EndDate, UserName = ur.UserName, Company = c.Name, Priority = p.Description, Problem = pr.Description, MaintenanceType = mt.Name, Description = m.Description}).ToListAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance>> GetMaintenanceById(int id){
            return await _context.Maintenances.FindAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("company/{email}")]
        public async Task<List<GetMaintenanceDto>> GetMaintenanceByCompanyId(string email){
            return await (from m in _context.Maintenances
                join e in _context.Equipments on m.EquipmentId equals e.Id
                join u in _context.Ubications on e.UbicationId equals u.Id
                join c in _context.Companies on u.CompanyId equals c.Id
                join ur in _context.Users on new {k1 = c.Id, k2 = m.User.Id} equals new {k1 = ur.CompanyId, k2 = ur.Id}
                join p in _context.Priorities on m.PriorityId equals p.Id
                join pr in _context.Problems on m.ProblemId equals pr.Id
                join mt in _context.MaintenanceTypes on m.MaintenanceTypeId equals mt.Id
                where ur.Email == email
                select new GetMaintenanceDto {Id = m.Id, DeviceName = e.DeviceName, StartDate = m.StartDate, EndDate = m.EndDate, UserName = ur.UserName, Company = c.Name, Priority = p.Description, Problem = pr.Name, MaintenanceType = mt.Name, Description = m.Description}).ToListAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostMaintenance(PostMaintenanceDto maintenanceDto)
        {
            if (!await EquipmentExists(maintenanceDto.EquipmentId))
            {
                return BadRequest();
            }

            var maintenance = new Maintenance
            {
                StartDate = maintenanceDto.StartDate,
                EndDate = maintenanceDto.EndDate,
                Description = maintenanceDto.Description,
                MaintenanceTypeId = maintenanceDto.MaintenanceTypeId,
                PriorityId = maintenanceDto.PriorityId,
                User = await _context.Users.Where(u => u.Email == maintenanceDto.UserEmail).FirstAsync(),
                ProblemId = maintenanceDto.ProblemId,
                EquipmentId = maintenanceDto.EquipmentId
            };

            _context.Maintenances.Add(maintenance);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenance(int id, Maintenance maintenance)
        {
            
            if(id != maintenance.Id){
                return BadRequest();
            }

            _context.Entry(maintenance).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        //
        [AllowAnonymous]
        [HttpPost("maintenance")]
        public async Task<IActionResult> UpdateMaintenance(UpdateMaintenanceDto maintenance)
        {
            
            if (!await MaintenanceExists(maintenance.Id))
            {
                return BadRequest();
            }

            // if(id != maintenance.Id){
            //     return BadRequest();
            // }

            var maint = new Maintenance 
            {
                Id = maintenance.Id,
                StartDate = maintenance.StartDate,
                EndDate = maintenance.EndDate,
                Description = maintenance.Description,
                MaintenanceTypeId = maintenance.MaintenanceTypeId,
                PriorityId = maintenance.PriorityId,
                ProblemId = maintenance.ProblemId
            };

            _context.Maintenances.Attach(maint);
            // _context.Entry(maint).State = EntityState.Modified;
            _context.Entry(maint).Property(x => x.StartDate).IsModified = true;
            _context.Entry(maint).Property(x => x.EndDate).IsModified = true;
            _context.Entry(maint).Property(x => x.Description).IsModified = true;
            _context.Entry(maint).Property(x => x.MaintenanceTypeId).IsModified = true;
            _context.Entry(maint).Property(x => x.PriorityId).IsModified = true;
            _context.Entry(maint).Property(x => x.ProblemId).IsModified = true;
            _context.SaveChanges();

            return Ok();
        }
        
        //api/equipment/id
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(int id)
        {
            var maintenance = await _context.Maintenances.FindAsync(id);
            
            if(maintenance == null){
                return NotFound();
            }

            _context.Maintenances.Remove(maintenance);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> EquipmentExists(int id){
            return await _context.Equipments.AnyAsync(x => x.Id == id);
        }

        private async Task<bool> MaintenanceExists(int id){
            return await _context.Maintenances.AnyAsync(x => x.Id == id);
        }
    }
}