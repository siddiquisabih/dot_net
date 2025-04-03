using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Tatweer.SafeCity.Authorization.Users;

namespace Tatweer.SafeCity.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public string[] Role { get; set; }

        public string[] Permissions { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string NIDNumber { get; set; }
        public string Rank { get; set; }

        public byte[] UserProfilePictureUrl { get; set; }

        public int? SupervisorId { get; set; }
    }
}
