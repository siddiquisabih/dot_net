using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Global.Project.Upload.Dto
{
    public class UploadFileDto 
    {

        public List<IFormFile> Files { get; set; }

    }
}
