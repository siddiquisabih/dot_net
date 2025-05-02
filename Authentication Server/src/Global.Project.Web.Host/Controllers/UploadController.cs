using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.StaticFiles;
using Abp.Domain.Uow;
using System.Transactions;
using log4net.Core;
using System.Net.Mail;
using Abp.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Global.Project.Model;
using Global.Project.Controllers;


namespace Global.Project.Web.Host.Controllers
{
  
    [Route("/api/services/app/[controller]/[action]")]
    public class UploadController : ProjectControllerBase
    {

        private IConfiguration _config;
        private readonly IRepository<ProjectAttachment> _attachmentReposioty;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedCache _distributedCache;
        public UploadController(IConfiguration config,
            IUnitOfWorkManager unitOfWorkManager, IRepository<ProjectAttachment> attachmentReposioty,
            IDistributedCache distributedCache)
        {
            _config = config;
            _unitOfWorkManager = unitOfWorkManager;
            _attachmentReposioty = attachmentReposioty;
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
