using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Global.Project.Roles.Dto;
using Global.Project.Users.Dto;

namespace Global.Project.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<UserDto> GetUserById(long id);
        Task<HttpStatusCode> CheckOTP(CheckOTPDto input);
        Task<HttpStatusCode> ChangePasswordByOTP(PasswordChangeRequestDto input);
        Task<HttpStatusCode> ExpireOldOTP(ExpireOldOTPDto input);
    }
}
