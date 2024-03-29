using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await EmailExists(registerDto.Email)) return BadRequest("Email is taken");

            using var hmac = new HMACSHA512();

            if (!await CompanyExists(registerDto.Company))
            {
                var company = new Company{
                    Name = registerDto.Company
                };
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();            
            }

            var c = await _context.Companies.Where(x => x.Name.ToLower() == registerDto.Company.ToLower()).FirstAsync();
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.FirstName + " " + registerDto.LastName,
                Email = registerDto.Email.ToLower(),
                Country = registerDto.Country,
                Telephone = registerDto.Telephone,
                Title = registerDto.Title,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                CompanyId = c.Id
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized("Invalidad email");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int p = 0; p < computedHash.Length; p++)
            {
                if (computedHash[p] != user.PasswordHash[p]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<bool> CompanyExists(string company){
            return await _context.Ubications.FromSqlRaw("SELECT * FROM dbo.Companies WHERE LOWER(Name) = LOWER('{0}')",company).AnyAsync();
        }
    }
}