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
    //api controller to demonstrate the basics of dot net core web api
    public class CustomersController : ControllerBase
    {
        static List<Customer> customers = new List<Customer>()
        {
            new Customer
                {
                    Id = 1,
                    FirstName = "Ljourn",
                    LastName = "Llagas",
                    Address = "Antipolo",
                    Age = 23,
                    Citizenship = "Filipino",
                    EmailAddress = "ljourn@gmail.com"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Juan",
                    LastName = "DelaCruz",
                    Address = "Pasay",
                    Age = 69,
                    Citizenship = "Japanese",
                    EmailAddress = "juan@gmail.com"
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Manny",
                    LastName = "Pacquiao",
                    Address = "Davao",
                    Age = 40,
                    Citizenship = "Filipino",
                    EmailAddress = "pacquaio@gmail.com"
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "Neneng",
                    LastName = "Bee",
                    Address = "Pasig",
                    Age = 17,
                    Citizenship = "Chinese",
                    EmailAddress = "neneng@gmail.com"
                }
        };

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var filteredCustomer = customers.Where(a => a.Id == id);

            return Ok(filteredCustomer);
        }

        [Route("GetCustomer")]
        public ActionResult<Customer> GetCustomerByQueryString([FromQuery] int id)
        {
            var filteredCustomer = customers.Where(a => a.Id == id);

            return Ok(filteredCustomer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            customers.Add(customer);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpDelete("{id}")]
        public ActionResult<Customer> RemoveCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            customers.Remove(customer);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomerDetails(Customer customer)
        {
            var customerObj = customers.FirstOrDefault(c => c.Id == customer.Id);

            if (customerObj == null)
                return NotFound();

            customerObj.FirstName = customer.FirstName;
            customerObj.LastName = customer.LastName;
            customerObj.Address = customer.Address;
            customerObj.Age = customer.Age;
            customerObj.Citizenship = customer.Citizenship;
            customerObj.EmailAddress = customer.EmailAddress;

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}