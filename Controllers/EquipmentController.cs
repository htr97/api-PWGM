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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentsByCompany(int id){
            
            var equipments = await _context.Equipments.FromSqlRaw("SELECT * FROM dbo.Equipments WHERE UbicationId IN (SELECT Id FROM dbo.Ubications WHERE CompanyId = {0})",id).ToListAsync();
            return equipments;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> PostEquipment(EquipmentDto equipmentDto)
        {
            
            if (await UbicationExists(equipmentDto.Ubication,equipmentDto.CompanyId) == true)
            {
                var ubication = new Ubication
                {
                    Name = equipmentDto.Ubication,
                    CompanyId = equipmentDto.CompanyId
                };

                _context.Ubications.Add(ubication);
                await _context.SaveChangesAsync();
            }
            
            var uid = await _context.Ubications.Where(u => u.CompanyId == equipmentDto.CompanyId && u.Name.ToLower() == equipmentDto.Ubication.ToLower()).FirstAsync();


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


        private async Task<bool> UbicationExists(string ubication, int company){
            return await _context.Ubications.FromSqlRaw("SELECT * FROM dbo.Ubications WHERE LOWER(Name) = LOWER('{0}') and CompanyId = {1}",ubication, company).AnyAsync();
        }

        // private async Task<List<Ubication>> GetUbication(string ubication, int company){
        //     return await _context.Ubications.Where(u => u.CompanyId == company && u.Name.ToLower() == ubication.ToLower()).FirstAsync();
        // }

        private async Task<bool> EquipmentExists(int id){
            return await _context.Equipments.AnyAsync(x => x.Id == id);
        }

    }
}