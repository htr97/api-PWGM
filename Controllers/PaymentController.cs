using System;
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
    public class PaymentController : BaseApiController
    {

        private readonly DataContext _context;
        public PaymentController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> PostPayment(PaymentDto paymentDto)
        {

            
            // var uid = await  _context.Users.FromSqlRaw("SELECT ub.Email, ub.CompanyId FROM dbo.Users u LOWER(u.Email) = LOWER({1})",paymentDto.UserEmail).FirstAsync();
             var uid = await _context.Users.Where(u => u.Email.ToLower() == paymentDto.UserEmail.ToLower()).FirstAsync();

            var payment = new Payment
                {
                    PaymentDate = DateTime.Now,
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    CompanyId = uid.CompanyId,
                    Status = "P"
                };

                _context.Payments.Add(payment);
                return await _context.SaveChangesAsync() > 0 ;

        }

        [HttpGet("{email}")]
        public async Task<List<GetPaymentDto>> GetPaymentByEmail(string email){
            
            
            return await (from p in _context.Payments
                join c in _context.Companies on p.CompanyId equals c.Id
                join u in _context.Users on p.CompanyId equals u.CompanyId
                where u.Email.ToLower() == email.ToLower() && p.Month == DateTime.Now.Month && p.Year == DateTime.Now.Year
                select new GetPaymentDto{Status = p.Status, CompanyId = c.Id, Company = c.Name}).ToListAsync().ConfigureAwait(false);
        }
    }
}