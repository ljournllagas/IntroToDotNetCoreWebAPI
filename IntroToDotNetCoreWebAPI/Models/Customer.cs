using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.Models
{
    public class Customer
    {
        public Customer()
        { }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Address { get; set; }

        [Range(18, 99, ErrorMessage = "Age must be 18 years and up")]
        public int Age { get; set; }

        public string Citizenship { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; }

    }
}
