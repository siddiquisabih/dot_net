using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tatweer.SafeCity.Authorization.Users;

namespace Tatweer.SafeCity.Users.Dto
{

    [AutoMapFrom(typeof(User))]
    public class MobileUserDto : EntityDto<long>
    {
        public MobileUserDto()
        {
            TotalTicketIssuedByMonthCount = new List<TotalTicketIssuedByMonthDto>();
        }
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
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string NIDNumber { get; set; }
        public string Rank { get; set; }

        public int TotalTicketsIssuedCount { get; set; }

      public  List<TotalTicketIssuedByMonthDto> TotalTicketIssuedByMonthCount { get; set; }
        public int IssuedWithDriverTicketCount { get; set; }
        public int IssuedWithoutDriverTicketCount { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public int? SupervisorId { get; set; }
    }

    public class TotalTicketIssuedByMonthDto
    {
        public string Month { get; set; }
        public int TotalTicketIssued { get; set; }
    }
}
