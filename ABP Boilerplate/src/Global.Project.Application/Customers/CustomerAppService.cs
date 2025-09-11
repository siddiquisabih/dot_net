using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.UI;
using Global.Project.Authorization.Roles;
using Global.Project.Authorization.Users;
using Global.Project.Common;
using Global.Project.DTOs;
using Global.Project.Editions;
using Global.Project.Emails;
using Global.Project.Lookups;
using Global.Project.Model.Customers;
using Global.Project.MultiTenancy;
using Global.Project.Customers.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Global.Project.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {

        private readonly IRepository<CustomerInformation> _customerInfoRepo;
        private readonly IRepository<CustomerDocument> _customerDocRepo;
        private readonly TenantManager _tenantManager;
        private readonly IAbpZeroDbMigrator _migrator;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly ProjectEmailManager _emailManager;
        private readonly IRepository<User, long> _userRepo;
        private readonly IRepository<Country> _countryRepo;
        private readonly IRepository<State> _stateRepo;
        private readonly IRepository<City> _cityRepo;
        private readonly IRepository<Region> _regionRepo;

        public CustomerAppService(
            IRepository<CustomerInformation> customerInfoRepo,
            IRepository<CustomerDocument> customerDocRepo,
            IRepository<User, long> userRepo,
            EditionManager editionManager,
            TenantManager tenantManager,
            IAbpZeroDbMigrator migrator,
            UserManager userManager,
            RoleManager roleManager,
           ProjectEmailManager emailManager,
            IRepository<Country> countryRepo,
            IRepository<State> stateRepo,
            IRepository<City> cityRepo,
            IRepository<Region> regionRepo)
        {
            _customerInfoRepo = customerInfoRepo;
            _customerDocRepo = customerDocRepo;
            _userRepo = userRepo;
            _editionManager = editionManager;
            _tenantManager = tenantManager;
            _migrator = migrator;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailManager = emailManager;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
            _regionRepo = regionRepo;

            LocalizationSourceName = ProjectConsts.LocalizationSourceName;
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        private async Task<int> CreateNewTenant(NewTenantInfoDto input)
        {

            try
            {
                var tenant = ObjectMapper.Map<Tenant>(input);
                var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    tenant.EditionId = defaultEdition.Id;
                }
                await _tenantManager.CreateAsync(tenant);
                await CurrentUnitOfWork.SaveChangesAsync();

                _migrator.CreateOrMigrateForTenant(tenant);

                using (CurrentUnitOfWork.SetTenantId(tenant.Id))
                {
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                    await CurrentUnitOfWork.SaveChangesAsync();

                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress);
                    await _userManager.InitializeOptionsAsync(tenant.Id);
                    CheckErrors(await _userManager.CreateAsync(adminUser, User.DefaultPassword));
                    await CurrentUnitOfWork.SaveChangesAsync();

                    CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                    await CurrentUnitOfWork.SaveChangesAsync();
                }

                return tenant.Id;

            }
            catch (Exception ex)
            {

                throw new UserFriendlyException(ex.Message);


            }

        }

        public async Task<int> Register(CreateCustomerInformationDto body)
        {

            try
            {
                var customerExist = await _customerInfoRepo.FirstOrDefaultAsync(x => x.Email == body.Email);

                if (customerExist != null)
                {
                    throw new UserFriendlyException($"{L("email_exist_try_another", body.Email)}");
                }

                if (string.IsNullOrWhiteSpace(body.CompanyName))
                {
                    throw new UserFriendlyException(L("company_name_required"));
                }
                body.Email = body.Email.ToLower();
                string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var companyName = body.CompanyName.ToLower();
                var removingCharNum = Regex.Replace(companyName, @"[^a-z0-9 ]", "");
                string tenancyName = Regex.Replace(removingCharNum, @"\s+", "_");

                var tenantInfo = new NewTenantInfoDto
                {
                    TenancyName = $"{tenancyName}_{timestamp}",
                    Name = body.Name,
                    AdminEmailAddress = body.Email,
                    ConnectionString = null,
                    IsActive = body.IsActive
                };

                var tenantId = await CreateNewTenant(tenantInfo);
                await CurrentUnitOfWork.SaveChangesAsync();
                 
                await CurrentUnitOfWork.SaveChangesAsync();

                var task = Task.Run(async () =>
                {
                    await _emailManager.SendAccountVerificationEmail(body.Email, body.Name, body.PhoneNumber);
                });

                var convertRequest = ObjectMapper.Map<CustomerInformation>(body);
                convertRequest.TenantId = tenantId;
                var customerId = await _customerInfoRepo.InsertAndGetIdAsync(convertRequest);
                return customerId;
            }

            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error", ex.Message)}");
            }

        }

        public async Task<PaginatedResult<CustomerInformationDto>> GetAll(PaginationInput Params)
        {
            try
            {
                var list = await _customerInfoRepo.GetAll()
                    .Include(x => x.CustomerDocuments)
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();

                var country = await _countryRepo.GetAllAsync();
                var state = await _stateRepo.GetAllAsync();
                var city = await _cityRepo.GetAllAsync();
                var region = await _regionRepo.GetAllAsync();

                var listDto = ObjectMapper.Map<List<CustomerInformationDto>>(list);
                var recordsCount = listDto.Count;

                foreach (var listRecord in listDto)
                {
                    var customer = list.First(x => x.Id == listRecord.Id);

                    listRecord.CountryName = country.FirstOrDefault(x => x.Id == customer.CountryId)?.Name;
                    listRecord.StateName = state.FirstOrDefault(x => x.Id == customer.StateId)?.Name;
                    listRecord.CityName = city.FirstOrDefault(x => x.Id == customer.CityId)?.Name;
                    listRecord.RegionName = region.FirstOrDefault(x => x.Id == customer.RegionId)?.Name;
                }

                listDto = listDto.ApplyQueryFilter(Params.Filters).ApplySorting(Params.SortBy, Params.IsDescending).ToList();

                var pagedResults = listDto
                   .Skip((Params.PageNumber - 1) * Params.PageSize)
                   .Take(Params.PageSize)
                   .ToList();

                return new PaginatedResult<CustomerInformationDto>
                {
                    TotalRecords = recordsCount,
                    Items = pagedResults,
                    TotalCount = listDto.Count,
                    PageNumber = Params.PageNumber,
                    PageSize = Params.PageSize
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error", ex.Message)}");
            }
        }

        public async Task<CustomerInformationDto> GetById(int Id)
        {

            if (Id <= 0)
            {
                throw new UserFriendlyException(L("no_record_found"));
            }

            try
            {
                var customer = await _customerInfoRepo.GetAll().Include(x => x.CustomerDocuments
                .Where(a => a.CustomerInformationId == Id))
                .FirstOrDefaultAsync(x => x.Id == Id);

                var customerDto = ObjectMapper.Map<CustomerInformationDto>(customer);

                var country = await _countryRepo.FirstOrDefaultAsync(x => x.Id == customer.CountryId);
                customerDto.CountryName = country?.Name;

                var state = await _stateRepo.FirstOrDefaultAsync(x => x.Id == customer.StateId);
                customerDto.StateName = state?.Name;

                var city = await _cityRepo.FirstOrDefaultAsync(x => x.Id == customer.CityId);
                customerDto.CityName = city?.Name;

                var region = await _regionRepo.FirstOrDefaultAsync(x => x.Id == customer.RegionId);
                customerDto.RegionName = region?.Name;


                return customerDto;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error", ex.Message)}");
            }
        }

        public async Task<int> Update(UpdateCustomerInformationDto updateBody)
        {

            if (updateBody.Id <= 0)
            {
                throw new UserFriendlyException(L("no_record_found"));
            }

            try
            {
                var customer = await _customerInfoRepo.GetAll()
                    .Include(x => x.CustomerDocuments)
                    .FirstOrDefaultAsync(x => x.Id == updateBody.Id) ?? throw new UserFriendlyException(L("no_record_found"));

                if (customer.IsActive != updateBody.IsActive)
                {
                    var tenant = await _tenantManager.GetByIdAsync(customer.TenantId);

                    using (CurrentUnitOfWork.SetTenantId(customer.TenantId))
                    {
                        var user = await _userRepo.FirstOrDefaultAsync(u => u.EmailAddress == customer.Email);
                        if (user != null)
                        {
                            user.IsActive = updateBody.IsActive;
                            await _userRepo.UpdateAsync(user);
                        }
                    }

                    if (tenant != null)
                    {
                        tenant.IsActive = updateBody.IsActive;
                        await _tenantManager.UpdateAsync(tenant);
                    }
                }


                var existingDocuments = customer.CustomerDocuments?.ToList() ?? new List<CustomerDocument>();

                ObjectMapper.Map(updateBody, customer);
                customer.Email = customer.Email;

                if (updateBody.CustomerDocuments != null)
                {
                    customer.CustomerDocuments = existingDocuments;


                    foreach (var updatedDoc in updateBody.CustomerDocuments.Where(d => d.Id > 0))
                    {
                        var existingDoc = customer.CustomerDocuments.FirstOrDefault(d => d.Id == updatedDoc.Id);
                        if (existingDoc != null)
                        {
                            existingDoc.OriginalFileName = updatedDoc.OriginalFileName;
                            existingDoc.SavedFileName = updatedDoc.SavedFileName;
                            existingDoc.FilePath = updatedDoc.FilePath;
                            existingDoc.FileType = updatedDoc.FileType;
                            existingDoc.ExpiryDate = updatedDoc.ExpiryDate;
                            existingDoc.FileSize = updatedDoc.FileSize;
                        }
                    }

                    foreach (var doc in updateBody.CustomerDocuments.Where(d => d.Id == 0))
                    {
                        customer.CustomerDocuments.Add(new CustomerDocument
                        {
                            OriginalFileName = doc.OriginalFileName,
                            SavedFileName = doc.SavedFileName,
                            FilePath = doc.FilePath,
                            FileType = doc.FileType,
                            ExpiryDate = doc.ExpiryDate,
                            CustomerInformationId = customer.Id,
                            FileSize = doc.FileSize
                        });
                    }

                }

                await _customerInfoRepo.UpdateAsync(customer);

                return customer.Id;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error", ex.Message)}");
            }
        }

        public async Task<bool> DeleteDocument(int DocId, int CustomerInfoId)
        {
            var docInfo = await _customerDocRepo
                .FirstOrDefaultAsync(a => a.CustomerInformationId == CustomerInfoId && a.Id == DocId)
                    ?? throw new UserFriendlyException(L("file_not_found"));
            try
            {
                await _customerDocRepo.DeleteAsync(a => a.CustomerInformationId == CustomerInfoId && a.Id == DocId);
                return true;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error", ex.Message)}");
            }
        }

        public async Task<bool> SendCustomerEmail(string Email, int CustomerInfoId)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new UserFriendlyException(L("email_is_required"));
            }
            if (CustomerInfoId <= 0)
            {
                throw new UserFriendlyException(L("customer_id_required"));
            }

            var customerInfo = await _customerInfoRepo
                .FirstOrDefaultAsync(x => x.Id == CustomerInfoId) ?? throw new UserFriendlyException(L("no_record_found"));

            var task = Task.Run(async () =>
            {
                await _emailManager.SendManualEmailToCustomer(Email, customerInfo.Email, customerInfo.Name, customerInfo.CompanyName);
            });
            return true;
        }
    
    
    }
}
