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
        public async Task<ActionResult<GetUserDto>> GetUser(string email){
            return await (from u in _context.Users where u.Email == email select new GetUserDto { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName ,UserName = u.UserName, Email = u.Email, Country = u.Country, Telephone = u.Telephone, DateofBirth = u.DateofBirth}).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpGet("company/{id}")]
        public async Task<List<GetUserDto>> GetUserByCompanyId(int id){
            return await (from u in _context.Users join c in _context.Companies on u.CompanyId equals c.Id where c.Id == id select new GetUserDto { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName ,UserName = u.UserName, Email = u.Email, Country = u.Country, Telephone = u.Telephone, DateofBirth = u.DateofBirth, CompanyId = c.Id , Company = c.Name}).ToListAsync().ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpPost("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto user)
        {
            var uId = await (from u in _context.Users where u.Email.ToLower() == user.Email.ToLower() select new { u.Id }).FirstOrDefaultAsync().ConfigureAwait(false);

            var _user = new AppUser 
            {
                Id = uId.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.FirstName + ' ' + user.LastName,
                DateofBirth = user.DateofBirth,
                Telephone = user.Telephone,
                Country = user.Country
            };

            _context.Users.Attach(_user);
            // _context.Entry(maint).State = EntityState.Modified;
            _context.Entry(_user).Property(x => x.FirstName).IsModified = true;
            _context.Entry(_user).Property(x => x.LastName).IsModified = true;
            _context.Entry(_user).Property(x => x.UserName).IsModified = true;
            _context.Entry(_user).Property(x => x.DateofBirth).IsModified = true;
            _context.Entry(_user).Property(x => x.Telephone).IsModified = true;
            _context.Entry(_user).Property(x => x.Country).IsModified = true;
            _context.Entry(_user).Property(x => x.Email).IsModified = false;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUserByEmail(string email, UpdateUserDto user)
        {
            
            if(email.ToLower() != user.Email.ToLower()){
                return BadRequest();
            }

            var entity = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            //entity.UserName = user.UserName;
            entity.Email = user.Email;
            entity.DateofBirth = user.DateofBirth;
            entity.Country = user.Country;
            entity.Telephone = user.Telephone;

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