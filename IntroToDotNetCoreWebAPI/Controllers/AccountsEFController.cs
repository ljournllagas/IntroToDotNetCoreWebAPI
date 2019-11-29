using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroToDotNetCoreWebAPI.Models;
using IntroToDotNetCoreWebAPI.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroToDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsEFController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public AccountsEFController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Account>> GetCustomers()
        {
            List<CustomerAccountDto> customerAccounts = new List<CustomerAccountDto>(); 

            var customerAccountObj = _dbContext.Account
                                        .Include(c => c.Customer)
                                        .ToList();

            foreach (var item in customerAccountObj)
            {
                var customerAccount = new CustomerAccountDto
                {
                    AccountName = item.AccountName,
                    AccountNumber = item.AccountNumber,
                    Address = item.Customer.Address,
                    EmailAddress = item.Customer.EmailAddress
                };

                customerAccounts.Add(customerAccount);
            }
            
            return Ok(customerAccounts);
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            //check if customerId already exists in the DB
            if (!_dbContext.Account.Where(a => a.CustomerId == account.CustomerId).Any())
                return StatusCode(StatusCodes.Status400BadRequest, 
                    new { Error = $"CustomerId >>> {account.CustomerId} does not exist" });

                _dbContext.Add(account);

            if (_dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}