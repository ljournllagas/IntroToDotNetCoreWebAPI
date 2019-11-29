using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.Models
{
    public class Account
    {
        public Account()
        {}

        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string AccountNumber { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
