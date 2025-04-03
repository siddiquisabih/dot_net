using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tatweer.SafeCity.Auditor.Model;
using Tatweer.SafeCity.Customer.Dto;
using Tatweer.SafeCity.Model.Customer;
using Tatweer.SafeCity.Models;

namespace Tatweer.SafeCity.Mappings
{
    public class AuditProfile: Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditLog, AuditLogModel>()
                .ForMember(d => d.AuditType, s => s.MapFrom(a => a.AuditType.AuditTypeName))
                .ForMember(d => d.UserName, s => s.MapFrom(a => a.User.Name))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.UserId))
                ;

            CreateMap<AuditType, AuditTypeModel>().ReverseMap();

            CreateMap<CustomerEntity, CreateCustomerDto>().ReverseMap();
            CreateMap<CustomerEntity, GetAllCustomersDto>().ReverseMap();

        }
    }
}
