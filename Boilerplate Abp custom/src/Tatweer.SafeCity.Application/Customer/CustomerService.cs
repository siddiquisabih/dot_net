using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper.Internal.Mappers;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tatweer.SafeCity.Authorization;
using Tatweer.SafeCity.Customer.Dto;
using Tatweer.SafeCity.Model.Customer;
using HubSpot.NET.Core;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Interfaces;
using System.IO;
namespace Tatweer.SafeCity.Customer
{
    public class CustomerService : ApplicationService, ICustomerService
    {

        private readonly IRepository<CustomerEntity> _customerRepository;
        public CustomerService(IRepository<CustomerEntity> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<List<GetAllCustomersDto>> GetAllCustomers()
        {
            var customer = await _customerRepository.GetAllListAsync();
            var customerDto = ObjectMapper.Map<List<GetAllCustomersDto>>(customer);
            return customerDto;
        }

        [HttpPost]
        public async Task CreateNewCustomer(CreateCustomerDto body)
        {
            var customer = ObjectMapper.Map<CustomerEntity>(body);
            await _customerRepository.InsertAsync(customer);
            return;
        }

        [HttpGet]
        
        //[AbpAuthorize(PermissionNames.GetContactCrm)]
        public async Task<List<ContactHubSpotModel>> GetAllCrmContacts()
        {

            var hubSpotApi = new HubSpotApi("//your crm hubspot api key");

            try
            {
                var contacts = hubSpotApi.Contact.List<ContactHubSpotModel>();
                return contacts.Contacts.ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error fetching HubSpot contacts: " + ex.Message);
                throw;
            }



        }
    }
}


