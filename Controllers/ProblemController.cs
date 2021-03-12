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
    public class ProblemController: BaseApiController
    {
        private readonly DataContext _context;
        public ProblemController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblems(){
            return await _context.Problems.ToListAsync();   
        }

        [AllowAnonymous]
        [HttpGet("company/{email}")]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblemsByCompanyId(string email){
            return await _context.Problems.FromSqlRaw("Select * FROM dbo.Problems WHERE CompanyId = (Select CompanyId from dbo.Users where LOWER(Email) = LOWER({0}))", email).ToListAsync();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostProblem(PostProblemDto problem)
        {

            var user = await _context.Users.Where(u => u.Email.ToLower() == problem.UserEmail.ToLower()).FirstAsync();
            var company = await _context.Companies.Where(c => c.Id == user.CompanyId).FirstAsync();

            var x = new Problem
            {
                Name = problem.Name,
                Description = problem.Description,
                Company = company
            };

            _context.Problems.Add(x);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblem(int id, Problem problem)
        {
            
            if(id != problem.Id){
                return BadRequest();
            }

            _context.Entry(problem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(int id){
            
            var problem = await _context.Problems.FindAsync(id);

            if (problem == null){
                return NotFound();
            }

            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}