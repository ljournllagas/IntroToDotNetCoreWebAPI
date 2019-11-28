using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IntroToDotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace IntroToDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //api controller to demonstrate the Dapper ORM
    public class CustomersDapperController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            using (var connection = DbConnection())
            {
                return Ok(connection.Query<Customer>("SELECT * FROM Customer"));
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            using (var connection = DbConnection())
            {
                var filteredCustomer = connection.Query<Customer>("SELECT * FROM Customer where id=@id", new { id });
                
                return Ok(filteredCustomer);
            }
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            using (var connection = DbConnection())
            {
                var query = "INSERT INTO Customer (FirstName, LastName, Address, Age, Citizenship, EmailAddress) " +
                   "VALUES (@firstName, @lastName, @address, @age, @citizenship, @email)";

                DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@firstName", customer.FirstName);
                    parameters.Add("@lastName", customer.LastName);
                    parameters.Add("@address", customer.Address);
                    parameters.Add("@age", customer.Age);
                    parameters.Add("@citizenship", customer.Citizenship);
                    parameters.Add("@email", customer.EmailAddress);

                var isInserted = connection.Execute(query, parameters);

                if (isInserted == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> RemoveCustomer(int id)
        {
            using (var connection = DbConnection())
            {
                var isDeleted = connection.Execute("DELETE FROM Customer WHERE id=@id", new { id });
                    
                if (isDeleted == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return StatusCode(StatusCodes.Status204NoContent);
            }

        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomerDetails(Customer customer)
        {

            using (var connection = DbConnection())
            {
                var customerObj = connection.Query<Customer>("SELECT * FROM Customer WHERE id=@id", new { @id = customer.Id });

                if (customerObj == null)
                    return NotFound();

                var query = "UPDATE Customer SET FirstName=@firstName, LastName=@lastName, Address=@address," +
                    "Age=@age, Citizenship=@citizenship, EmailAddress=@email where Id=@Id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", customer.Id);
                parameters.Add("@firstName", customer.FirstName);
                parameters.Add("@lastName", customer.LastName);
                parameters.Add("@address", customer.Address);
                parameters.Add("@age", customer.Age);
                parameters.Add("@citizenship", customer.Citizenship);
                parameters.Add("@email", customer.EmailAddress);

                var isInserted = connection.Execute(query, parameters);

                if (isInserted == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return StatusCode(StatusCodes.Status204NoContent);
            }


           
        }

        private IDbConnection DbConnection()
        {
            IDbConnection connection = new SqliteConnection("Data source = IntroToWebApi.db");

            return connection;
        }
    }
}