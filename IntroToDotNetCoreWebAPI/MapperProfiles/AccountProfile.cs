using AutoMapper;
using IntroToDotNetCoreWebAPI.Models;
using IntroToDotNetCoreWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToDotNetCoreWebAPI.MapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, CustomerAccountDto>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Customer.EmailAddress))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address));
        }
    }
}
