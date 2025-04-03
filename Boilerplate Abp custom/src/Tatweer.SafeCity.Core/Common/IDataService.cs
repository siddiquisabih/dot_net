using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.Common
{
    public interface IDataService: IApplicationService
    {
        void RefreshMapping();
        void RefreshDepartmentMapping();
        List<UserDepartmentSupervisorDto> Get();
    }
}
