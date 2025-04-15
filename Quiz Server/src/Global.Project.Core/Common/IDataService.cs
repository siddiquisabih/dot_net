using Abp.Application.Services;
using Global.Project.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Common
{
    public interface IDataService: IApplicationService
    {
        void RefreshMapping();
        void RefreshDepartmentMapping();
        List<UserDepartmentSupervisorDto> Get();
    }
}
