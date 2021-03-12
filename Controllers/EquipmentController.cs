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
    public class EquipmentController : BaseApiController
    {
        private readonly DataContext _context;
        public EquipmentController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipments(){
            return await _context.Equipments.ToListAsync();   
        }

        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentsByCompany(string email){
            
            var equipments = await _context.Equipments.FromSqlRaw("SELECT e.* FROM dbo.Equipments e INNER JOIN dbo.Ubications u on e.UbicationId = u.Id INNER JOIN dbo.Companies c on u.CompanyId = c.Id WHERE c.Id = (SELECT ur.CompanyId FROM dbo.Users ur WHERE LOWER(ur.Email)  = LOWER({0}))",email).ToListAsync();
            return equipments;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> PostEquipment(EquipmentDto equipmentDto)
        {
            
            if (await UbicationExists(equipmentDto.Ubication,equipmentDto.UserEmail) != true)
            {
                var email = await _context.Users.Where(u => u.Email.ToLower() == equipmentDto.UserEmail.ToLower()).FirstAsync();

                var ubication = new Ubication
                {
                    Name = equipmentDto.Ubication,
                    CompanyId = email.CompanyId
                };

                _context.Ubications.Add(ubication);
                await _context.SaveChangesAsync();
            }
            
            // var uid = await  _context.Ubications.FromSqlRaw("SELECT ub.Id, ub.CompanyId, ub.Name FROM dbo.Ubications ub inner join PWGM.dbo.Users u on ub.CompanyId = u.CompanyId WHERE LOWER(ub.Name) = LOWER('{0}') and LOWER(u.Email) = LOWER({1})",equipmentDto.Ubication, equipmentDto.UserEmail).FirstAsync();
            var uid = await (from ub in _context.Ubications join u in _context.Users on ub.CompanyId equals u.CompanyId where ub.Name.ToLower() == equipmentDto.Ubication.ToLower() && u.Email.ToLower() == equipmentDto.UserEmail.ToLower() select new { ub.Id, ub.CompanyId, ub.Name}).FirstOrDefaultAsync().ConfigureAwait(false);

            var equipment = new Equipment
                {
                    DeviceName = equipmentDto.DeviceName,
                    SystemType = equipmentDto.SystemType,
                    StorageType = equipmentDto.StorageType,
                    StorageCap = equipmentDto.StorageCap,
                    Processor = equipmentDto.Processor,
                    Memory = equipmentDto.Memory,
                    OsName = equipmentDto.OsName,
                    Observation = equipmentDto.Observation,
                    UbicationId = uid.Id
                };

                _context.Equipments.Add(equipment);
                return await _context.SaveChangesAsync() > 0 ;

        }

        //api/equipment/id
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        {
            
            if(id != equipment.Id){
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        //api/equipment/id
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            
            if(equipment == null){
                return NotFound();
            }

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return Ok();
        }


        private async Task<bool> UbicationExists(string ubication, string email){
            return await (from ub in _context.Ubications join u in _context.Users on ub.CompanyId equals u.CompanyId where ub.Name.ToLower() == ubication.ToLower() && u.Email.ToLower() == email.ToLower() select new { ub.Id, ub.CompanyId, ub.Name}).AnyAsync();
        }

        private async Task<bool> EquipmentExists(int id){
            return await _context.Equipments.AnyAsync(x => x.Id == id);
        }

    }
}