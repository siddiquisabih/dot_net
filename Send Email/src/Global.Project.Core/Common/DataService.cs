using Abp.Domain.Repositories;
using Global.Project.DTOs;
using Global.Project.Model.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Global.Project.Common
{
    public class DataService : IDataService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserDepartmentSupervisor> _departmentUserRepository;
        public DataService(IRepository<UserDepartmentSupervisor> departmentUserRepository, IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor)
        {
            this.memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            _departmentUserRepository = departmentUserRepository;
        }
        public void RefreshMapping()
        {
            var context = _httpContextAccessor.HttpContext;

            var baseUrl = $"{context.Request.Scheme}://{context.Request.Headers["Host"]}";
            using (var client = new HttpClient())
            {
              
                client.BaseAddress = new Uri($"{baseUrl}/api/services/app/Department/RefreshMapping");
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
            }

        }


        public void RefreshDepartmentMapping()
        {
            var cacheKey = ProjectConsts.CacheKeyUsersDepartment;
            var data = _departmentUserRepository.GetAll().ToList();
            var cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(24),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
            memoryCache.Set(cacheKey, JsonSerializer.Serialize(data), cacheExpirationOptions);
        }



        public List<UserDepartmentSupervisorDto> Get()
        {
            List<UserDepartmentSupervisorDto> resultMapping = null;
            return resultMapping;
        }

        public bool IsMappingExists()
        {
            return memoryCache.TryGetValue(ProjectConsts.CacheKeyUsersDepartment, out List<UserDepartmentSupervisorDto> mappingList);
        }
    }
}
