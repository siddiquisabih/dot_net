using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Common.Helpers;
using Global.Project.Upload.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Global.Project.Upload
{
    public interface IUploadAppService : IApplicationService
    {

        Task<List<FileUploadResult>> UploadFile([FromForm] UploadFileDto formBody);

        string GetUploadedFile(string fileName);

        Task<bool> DeleteFile(string fileName);

    }
}
