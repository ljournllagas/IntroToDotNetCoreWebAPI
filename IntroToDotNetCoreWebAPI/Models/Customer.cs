using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.Models
{
    public class Customer
    {
        public Customer()
        { }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Citizenship { get; set; }
        public string EmailAddress { get; set; }


    }
}
