using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.DTOs;
using Global.Project.Lookups.Dto;

namespace Global.Project.Lookups
{
    public interface ILookupAppService : IApplicationService
    {

        Task<PaginatedResult<RegionDto>> GetAllRegions(PaginationInput Params);

        Task<PaginatedResult<CountryDto>> GetAllCountries(PaginationInput Params);

        Task<PaginatedResult<CountryDto>> GetCountiesByRegionId(int regionId, PaginationInput Params);

        Task<PaginatedResult<StateDto>> GetStatesByCountryId(int countryId, PaginationInput Params);

        Task<PaginatedResult<CityDto>> GetCitiesByStateId(int stateId, PaginationInput Params);

        Task<PaginatedResult<CityDto>> GetCitiesByCountryId(int countryId, PaginationInput Params);
    }
}
