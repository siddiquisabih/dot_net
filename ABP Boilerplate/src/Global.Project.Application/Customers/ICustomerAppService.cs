using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.DTOs;
using Global.Project.Customers.Dto;

namespace Global.Project.Customers
{
    public interface ICustomerAppService : IApplicationService
    {


        Task<int> Register(CreateCustomerInformationDto body);

        Task<PaginatedResult<CustomerInformationDto>> GetAll(PaginationInput Params);

        Task<CustomerInformationDto> GetById(int Id);

        Task<int> Update(UpdateCustomerInformationDto updateBody);

        Task<bool> DeleteDocument(int DocId, int CustomerInfoId);

        Task<bool> SendCustomerEmail(string Email, int CustomerInfoId);


    }
}
