using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Global.Project.Common;
using Global.Project.DTOs;
using Global.Project.Lookups.Dto;
using Microsoft.EntityFrameworkCore;

namespace Global.Project.Lookups
{
    [AbpAuthorize]
    public class LookupAppService : ApplicationService, ILookupAppService
    {

        private readonly IRepository<Region> _regionRepo;
        private readonly IRepository<Country> _countryRepo;
        private readonly IRepository<State> _stateRepo;
        private readonly IRepository<City> _cityRepo;


        public LookupAppService(
            IRepository<Region> regionRepo,
            IRepository<Country> countryRepo,
            IRepository<State> stateRepo,
            IRepository<City> cityRepo)
        {
            _regionRepo = regionRepo;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
            LocalizationSourceName = ProjectConsts.LocalizationSourceName;

        }


        public async Task<PaginatedResult<RegionDto>> GetAllRegions(PaginationInput Params)
        {


            var regionList = await _regionRepo.GetAll()
               .Where(x => x.IsActive == true)
               .ToListAsync();
            var convertRegion = ObjectMapper.Map<List<RegionDto>>(regionList);
            var recordsCount = convertRegion.Count;

            var pagedResults = convertRegion
               .Skip((Params.PageNumber - 1) * Params.PageSize)
               .Take(Params.PageSize)
               .ToList();

            return new PaginatedResult<RegionDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertRegion.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };
        }

        public async Task<PaginatedResult<CountryDto>> GetAllCountries(PaginationInput Params)
        {

            var regionList = await _countryRepo.GetAll()
              .Where(x => x.IsActive == true)
              .ToListAsync();
            var convertCounties = ObjectMapper.Map<List<CountryDto>>(regionList);
            var recordsCount = convertCounties.Count;

            var pagedResults = convertCounties
               .Skip((Params.PageNumber - 1) * Params.PageSize)
               .Take(Params.PageSize)
               .ToList();

            return new PaginatedResult<CountryDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertCounties.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };
        }

        public async Task<PaginatedResult<CountryDto>> GetCountiesByRegionId(int regionId, PaginationInput Params)
        {

            var countryList = await _countryRepo.GetAll()
                  .Where(x => x.RegionId == regionId && x.IsActive == true)
                  .ToListAsync();

            var convertCounties = ObjectMapper.Map<List<CountryDto>>(countryList);
            var recordsCount = convertCounties.Count;

            var pagedResults = convertCounties
               .Skip((Params.PageNumber - 1) * Params.PageSize)
               .Take(Params.PageSize)
               .ToList();

            return new PaginatedResult<CountryDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertCounties.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };



        }

        public async Task<PaginatedResult<StateDto>> GetStatesByCountryId(int countryId, PaginationInput Params)
        {

            var stateList = await _stateRepo.GetAll()
                  .Where(x => x.CountryId == countryId && x.IsActive == true)
                  .ToListAsync();

            var convertState = ObjectMapper.Map<List<StateDto>>(stateList);
            var recordsCount = convertState.Count;

            var pagedResults = convertState
               .Skip((Params.PageNumber - 1) * Params.PageSize)
               .Take(Params.PageSize)
               .ToList();

            return new PaginatedResult<StateDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertState.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };

        }

        public async Task<PaginatedResult<CityDto>> GetCitiesByStateId(int stateId, PaginationInput Params)
        {

            var cityList = await _cityRepo.GetAll()
               .Where(x => x.StateId == stateId && x.IsActive == true)
               .ToListAsync();
            var convertCity = ObjectMapper.Map<List<CityDto>>(cityList);

            var recordsCount = convertCity.Count;

            convertCity = convertCity.ApplyQueryFilter(Params.Filters).ApplySorting(Params.SortBy, Params.IsDescending).ToList();

            var pagedResults = convertCity
                .Skip((Params.PageNumber - 1) * Params.PageSize)
                .Take(Params.PageSize)
                .ToList();

            return new PaginatedResult<CityDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertCity.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };
        }

        public async Task<PaginatedResult<CityDto>> GetCitiesByCountryId(int countryId, PaginationInput Params)
        {
            var cityList = await _cityRepo.GetAll()
               .Where(x => x.CountryId == countryId && x.IsActive == true)
               .ToListAsync();

            var convertCity = ObjectMapper.Map<List<CityDto>>(cityList);
            var recordsCount = convertCity.Count;

            convertCity = convertCity.ApplyQueryFilter(Params.Filters).ApplySorting(Params.SortBy, Params.IsDescending).ToList();

            var pagedResults = convertCity
               .Skip((Params.PageNumber - 1) * Params.PageSize)
               .Take(Params.PageSize)
               .ToList();

            return new PaginatedResult<CityDto>
            {
                TotalRecords = recordsCount,
                Items = pagedResults,
                TotalCount = convertCity.Count,
                PageNumber = Params.PageNumber,
                PageSize = Params.PageSize
            };
        }
    }
}
