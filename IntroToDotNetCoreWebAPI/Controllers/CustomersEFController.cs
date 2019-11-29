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
        private readonly AppDbContext dbContext;

        public CustomersEFController(AppDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(dbContext.Customer.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var filteredCustomer = dbContext.Customer.FirstOrDefault(c => c.Id == id);
            return Ok(filteredCustomer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            dbContext.Add(customer);

            if (dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> RemoveCustomer(int id)
        {
            var filteredCustomer = dbContext.Customer.FirstOrDefault(c => c.Id == id);

            dbContext.Customer.Remove(filteredCustomer);

            if (dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomerDetails(Customer customer)
        {
            var customerObj = dbContext.Customer.FirstOrDefault(c => c.Id == customer.Id);

            if (customerObj == null)
                return NotFound();

            customerObj.FirstName = customer.FirstName;
            customerObj.LastName = customer.LastName;
            customerObj.Address = customer.Address;
            customerObj.Age = customer.Age;
            customerObj.Citizenship = customer.Citizenship;
            customerObj.EmailAddress = customer.EmailAddress;

            if (dbContext.SaveChanges() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}