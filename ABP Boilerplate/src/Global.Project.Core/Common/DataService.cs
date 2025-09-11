using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;


namespace Global.Project.Common
{
    public class DataService : IDataService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataService( IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor)
        {
            this.memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }


        



        
       
    }
}
