﻿using Abp.Application.Services.Dto;

namespace Tatweer.SafeCity.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

