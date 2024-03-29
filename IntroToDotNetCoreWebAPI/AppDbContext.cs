﻿ using IntroToDotNetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Account> Account { get; set; }

    }
}
