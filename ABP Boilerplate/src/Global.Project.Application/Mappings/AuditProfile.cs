using AutoMapper;
using Global.Project.Authorization.Users;
using Global.Project.Lookups;
using Global.Project.Model;
using Global.Project.Model.Customers;
using Global.Project.Auditor.Model;
using Global.Project.Customers.Dto;
using Global.Project.Lookups.Dto;
using Global.Project.Users.Dto;

namespace Global.Project.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditLog, AuditLogModel>()
                .ForMember(d => d.AuditType, s => s.MapFrom(a => a.AuditType.AuditTypeName))
                .ForMember(d => d.UserName, s => s.MapFrom(a => a.User.Name))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.UserId))
                ;

            CreateMap<AuditType, AuditTypeModel>().ReverseMap();


            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<CustomerInformation, CustomerInformationDto>().ReverseMap();
            CreateMap<CustomerInformation, CreateCustomerInformationDto>().ReverseMap();
            CreateMap<CustomerInformation, UpdateCustomerInformationDto>().ReverseMap();
            CreateMap<CustomerDocument, CustomerDocumentDto>().ReverseMap();
            CreateMap<CustomerDocument, CreateCustomerDocumentDto>().ReverseMap();
            CreateMap<CustomerDocument, UpdateCustomerDocumentDto>().ReverseMap();

            CreateMap<CreateUserProfileImageDto, UserProfileImage>().ReverseMap();
            CreateMap<UserProfileImageDto, UserProfileImage>().ReverseMap();

        }
    }
}
