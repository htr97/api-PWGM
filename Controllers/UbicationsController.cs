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
    public class UbicationsController: BaseApiController
    {
        private readonly DataContext _context;
        public UbicationsController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<UbicationDto>> GetUbication(){
            return await (from u in _context.Ubications join c in _context.Companies on u.CompanyId equals c.Id select new UbicationDto { Id = u.Id, Name = u.Name, CompanyId = c.Id , Company = c.Name}).ToListAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<List<UbicationDto>> GetUbicationByCompany(int id){
            return await (from u in _context.Ubications join c in _context.Companies on u.CompanyId equals c.Id where c.Id == id select new UbicationDto { Id = u.Id, Name = u.Name, CompanyId = c.Id , Company = c.Name}).ToListAsync().ConfigureAwait(false);
        }
        
    }
}