using Abp.AutoMapper;
using Tatweer.ITSM.Authentication.External;

namespace Tatweer.ITSM.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
