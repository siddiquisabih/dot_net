using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Localization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Global.Project.Common.Helpers
{
    public static class FileHelper
    {
        private const long DEFAULT_MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB


        private static IConfiguration _config;
        public static void Initialize(IConfiguration config)
        {
            _config = config;
        }

        public static string L(string name, params object[] args)
        {
            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
            var localizedText = localizationManager.GetString(ProjectConsts.LocalizationSourceName, name);
            return args != null && args.Length > 0
            ? string.Format(localizedText, args)
            : localizedText;
        }

        private static string GetBaseUploadPath()
        {
            var configPath = _config.GetValue<string>("Paths:FileUploadPath");
            return configPath;
        }

        public static async Task<List<FileUploadResult>> SaveFilesAsync(
            IList<IFormFile> files,
            int? maxFiles = null,
            long? maxFileSize = null)
        {
            var results = new List<FileUploadResult>();
            var baseUploadPath = Path.Combine(GetBaseUploadPath());

            if (!Directory.Exists(baseUploadPath))
                Directory.CreateDirectory(baseUploadPath);

            if (maxFiles.HasValue && files.Count > maxFiles.Value)
                throw new UserFriendlyException(L("Maximum_files_are_allowed", maxFiles.Value));


            foreach (var file in files)
            {
                if (file.Length > (maxFileSize ?? DEFAULT_MAX_FILE_SIZE))
                    throw new UserFriendlyException(L("File_exceeds_maximum_size_limit", file.FileName));

                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(GetBaseUploadPath(), uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                results.Add(new FileUploadResult
                {
                    OriginalFileName = file.FileName,
                    SavedFileName = uniqueFileName,
                    FilePath = filePath,
                    FileSize = file.Length,
                    FileType = Path.GetExtension(file.FileName),
                });
            }

            return results;
        }

        public static bool DeleteFile(string fileName)
        {

            var filePath = Path.Combine(GetBaseUploadPath(), fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetFileBaseUrl()
        {
            return _config.GetValue<string>("Paths:BaseFilePath");
        }

        public static string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;
            return Path.Combine(GetFileBaseUrl(), fileName).Replace("\\", "/");
        }

        public static FileInfo GetFileInfo(string fileName)
        {
            var filePath = Path.Combine(GetBaseUploadPath(), fileName);
            return File.Exists(filePath) ? new FileInfo(filePath) : null;
        }

    }


    public class FileUploadResult
    {
        public string OriginalFileName { get; set; }
        public string SavedFileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
    }
}