using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.Models.Dtos
{
    public class CustomerAccountDto
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
    }
}
