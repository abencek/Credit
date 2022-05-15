using AutoMapper;
using Credit.Api.Dtos;
using Credit.Data.App.Models;
using Microsoft.AspNetCore.Identity;

namespace Credit.Api.Mapping
{
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Mapping profile configuration for AutoMapper
        /// </summary>
        public MapperProfile()
        {
            //Agreement
            CreateMap<Agreement, AgreementDto>();
            CreateMap<AgreementDto, Agreement>();

            //Customer
            CreateMap<CustomerStatus, CustomerDto>();
            CreateMap<Customer, CustomerDto>().IncludeMembers(s=>s.CustomerStatus);
            CreateMap<CustomerDto, Customer>();

            //Identity User
            CreateMap<IdentityUserDto, IdentityUser>();

            //Identity Role
            CreateMap<IdentityRoleDto, IdentityRole>();
        }
              
    }
}
