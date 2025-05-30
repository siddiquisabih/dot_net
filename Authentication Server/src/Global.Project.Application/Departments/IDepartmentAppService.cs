﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Global.Project.Departments.Dto;

namespace Global.Project.Departments
{ 
    public interface IDepartmentAppService : IApplicationService
    {
        bool CreateDepartment(DepartmentDto departmentDto);
        bool UpdateDepartment(DepartmentDto departmentDto);
        ListResultDto<DepartmentDto> GetAllDepartments(string keyWord);
        DepartmentDto GetDepartmentById(int id);
        bool DeleteDepartment(int id);
        bool AddDepartmentUserMapping(DepartmentUserMappingDto departmentUserMappingDto);
        ListResultDto<DepartmentUserMappingDto> GetUserDepartmentMapping(UserDepartmentServiceInputDto serviceInputDto);
    }
}