using FluentValidation;
using IntroToDotNetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly AppDbContext _dbContext;

        public AccountValidator(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.AccountName)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);

            RuleFor(x => x.AccountNumber)
               .NotEmpty()
               .NotNull()
               .MaximumLength(15)
               .MinimumLength(12);

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .NotNull()
                .Must(custId => HasExistingCustomerId(custId))
                .WithMessage(custId => $"Cannot create an account as customerId: [{custId.CustomerId}] does not exist");
        }

        private bool HasExistingCustomerId(int customerId)
        {
            var isExistingCustomerId = _dbContext.Customer.FirstOrDefault(c => c.Id == customerId);

            return (isExistingCustomerId == null ? false : true);
        }


    }
}
