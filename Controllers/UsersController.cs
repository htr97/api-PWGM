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

    public class UsersController: BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context){
            _context=context;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return await _context.Users.ToListAsync();
        }

        //api/users/email
        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("company/{id}")]
        public async Task<List<GetUserDto>> GetUserByCompanyId(int id){
            return await (from u in _context.Users join c in _context.Companies on u.CompanyId equals c.Id where c.Id == id select new GetUserDto { Id = u.Id, UserName = u.UserName, Email = u.Email, Country = u.Country, Telephone = u.Telephone, DateofBirth = u.DateofBirth, CompanyId = c.Id , Company = c.Name}).ToListAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUserByEmail(string email, UpdateUserDto user)
        {
            
            if(email.ToLower() != user.Email.ToLower()){
                return BadRequest();
            }

            var entity = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            entity.UserName = user.UserName;
            entity.Email = user.Email;
            entity.DateofBirth = user.DateofBirth;
            entity.Country = user.Country;
            entity.Telephone = user.Telephone;
            entity.Photo = user.Photo;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            
            if(user == null){
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}