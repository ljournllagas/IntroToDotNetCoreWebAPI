using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroToDotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroToDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersEFController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CustomersEFController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(_dbContext.Customer.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var filteredCustomer = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            return Ok(filteredCustomer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            _dbContext.Add(customer);

            if (_dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> RemoveCustomer(int id)
        {
            var filteredCustomer = _dbContext.Customer.FirstOrDefault(c => c.Id == id);

            _dbContext.Customer.Remove(filteredCustomer);

            if (_dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomerDetails(Customer customer)
        {
            var customerObj = _dbContext.Customer.FirstOrDefault(c => c.Id == customer.Id);

            if (customerObj == null)
                return NotFound();

            customerObj.FirstName = customer.FirstName;
            customerObj.LastName = customer.LastName;
            customerObj.Address = customer.Address;
            customerObj.Age = customer.Age;
            customerObj.Citizenship = customer.Citizenship;
            customerObj.EmailAddress = customer.EmailAddress;

            if (_dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}