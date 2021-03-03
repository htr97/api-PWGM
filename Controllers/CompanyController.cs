using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    public class CompanyController : BaseApiController
    {
        private readonly DataContext _context;
        public CompanyController(DataContext context)
        {
             _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies(){
            return await _context.Companies.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(int id){
            return await _context.Companies.FindAsync(id);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            
            if(id != company.Id){
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            
            if(company == null){
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}