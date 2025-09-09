using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Abp.Domain.Uow;
using Microsoft.Extensions.Caching.Distributed;
using Global.Project.Controllers;


namespace Global.Project.Web.Host.Controllers
{
  
    [Route("/api/services/app/[controller]/[action]")]
    public class UploadController : ProjectControllerBase
    {

        private IConfiguration _config;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedCache _distributedCache;
        public UploadController(IConfiguration config,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedCache distributedCache)
        {
            _config = config;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedCache = distributedCache;
        }


        [HttpGet]
        public async Task<IActionResult> DownloadExportToExcelTempFile(string fileToken, string fileName, string contentType)
        {
            var file = await _distributedCache.GetAsync(fileToken);
            await _distributedCache.RemoveAsync(fileToken);
            return File(file, contentType, fileName);
        }



        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


    }
}
