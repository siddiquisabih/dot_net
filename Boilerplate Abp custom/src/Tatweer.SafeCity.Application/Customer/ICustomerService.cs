using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using HubSpot.NET.Api.Contact.Dto;
using Tatweer.SafeCity.Customer.Dto;

namespace Tatweer.SafeCity.Customer
{
    public interface ICustomerService : IApplicationService
    {

        Task<List<GetAllCustomersDto>> GetAllCustomers();
        Task CreateNewCustomer(CreateCustomerDto body);
        Task<List<ContactHubSpotModel>> GetAllCrmContacts();
  

    }
}