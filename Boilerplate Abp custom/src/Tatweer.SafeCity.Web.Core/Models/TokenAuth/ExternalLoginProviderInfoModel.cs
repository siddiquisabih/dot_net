using Abp.AutoMapper;
using Tatweer.SafeCity.Authentication.External;

namespace Tatweer.SafeCity.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
