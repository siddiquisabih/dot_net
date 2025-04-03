using Microsoft.AspNetCore.Http;

namespace Tatweer.SafeCity.Web.Host.ViewModels
{
    public class MediaUploadModel
    {
        public IFormFile File { get; set; }
        public int AccidentId { get; set; }
    }
}
