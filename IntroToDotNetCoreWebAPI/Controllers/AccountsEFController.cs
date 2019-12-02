using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AccountsEFController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Account>> GetCustomers()
        {
            
            var customerAccountObj = _dbContext.Account
                                        .Include(c => c.Customer)
                                        .ToList();

            List<CustomerAccountDto> customerAccounts = _mapper.Map<List<CustomerAccountDto>>(customerAccountObj);

            //List<CustomerAccountDto> customerAccounts = new List<CustomerAccountDto>();

            //foreach (var item in customerAccountObj)
            //{
            //    var customerAccount = new CustomerAccountDto
            //    {
            //        AccountName = item.AccountName,
            //        AccountNumber = item.AccountNumber,
            //        Address = item.Customer.Address,
            //        EmailAddress = item.Customer.EmailAddress
            //    };

            //    customerAccounts.Add(customerAccount);
            //}

            return Ok(customerAccounts);
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            _dbContext.Add(account);

            if (_dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}