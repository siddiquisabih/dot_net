using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.UI;
using Global.Project.Common.Helpers;
using Global.Project.Upload.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Global.Project.Upload
{
    public class UploadAppService : ApplicationService, IUploadAppService
    {
        public async Task<List<FileUploadResult>> UploadFile([FromForm] UploadFileDto formBody)
        {

            if (formBody.Files == null)
            {
                throw new UserFriendlyException("Upload at least 1 file");
            }

            try
            {
                var uploadResults = await FileHelper.SaveFilesAsync(
                    files: formBody.Files,
                    maxFiles: 3
                );
                await CurrentUnitOfWork.SaveChangesAsync();
                return uploadResults;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetUploadedFile(string fileName)
        {
            if (fileName == null)
            {
                throw new UserFriendlyException("Provide File Name");
            }
            string fileUrl = FileHelper.GetFileUrl(fileName);
            return fileUrl;
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new UserFriendlyException("File Name Name is required");
            }

            var deleteResult = FileHelper.DeleteFile(fileName: fileName);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (deleteResult == false)
            {
                throw new UserFriendlyException($"File not found in storage");
            }

            return deleteResult;
        }

    }
}
