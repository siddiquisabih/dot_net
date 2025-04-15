using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Global.Project;
using Global.Project.Common;
using Global.Project.Departments.Dto;
using Global.Project.Model.Departments;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Global.Project.Departments
{
    public class DepartmentAppService :ProjectAppServiceBase, IDepartmentAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<UserDepartmentSupervisor> _departmentUserRepository;
        private readonly IDataService _dataService;

        private readonly IAbpSession _abpSession;

        public DepartmentAppService(IRepository<Department> department, IRepository<UserDepartmentSupervisor> departmentUser,
            IDataService dataService,

            IAbpSession abpSession)
        //: base(repository)
        {
            _departmentRepository = department;
            _departmentUserRepository = departmentUser;
            _dataService = dataService;
            _abpSession = abpSession;
        }

        public bool CreateDepartment(DepartmentDto departmentDto)
        {
            return CreateUpdateDepartment(departmentDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public bool UpdateDepartment(DepartmentDto departmentDto)
        {
            return CreateUpdateDepartment(departmentDto);
        }

        public ListResultDto<DepartmentDto> GetAllDepartments(string keyWord)
        {
            var departments = _departmentRepository.GetAll().Where(x => !string.IsNullOrWhiteSpace(keyWord) ? x.Name.Contains(keyWord) : true).ToList();
            if (departments != null && departments.Count > 0)
            {
                var departmentList = departments.Select(x => new DepartmentDto(x.Id, x.Code, x.Name, x.Description, x.ParentCode, !string.IsNullOrWhiteSpace(x.ParentCode) ? false : true, x.IsDeleted)).ToList();
                return new ListResultDto<DepartmentDto>(departmentList);
            }
            return new ListResultDto<DepartmentDto>(new List<DepartmentDto>());
        }

        public DepartmentDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department != null)
            {
                return new DepartmentDto(department.Id, department.Code, department.Name, department.Description, department.ParentCode, !string.IsNullOrWhiteSpace(department.ParentCode) ? true : false, department.IsDeleted);
            }
            return null;
        }

        public bool DeleteDepartment(int id)
        {
            var response = false;
            var department = _departmentRepository.Get(id);
            if (department != null)
            {
                department.IsDeleted = true;
                _departmentRepository.InsertOrUpdate(department);
                response = true;
            }
            return response;
        }

        public bool AddDepartmentUserMapping(DepartmentUserMappingDto departmentUserMappingDto)
        {
            var response = false;
            if (departmentUserMappingDto != null)
            {
                var departmentUserMapping = new UserDepartmentSupervisor
                {
                    Id = departmentUserMappingDto.Id,
                    UserId = departmentUserMappingDto.UserId,
                    SupervisorId = departmentUserMappingDto.SupervisorId,
                    DepartmentId = departmentUserMappingDto.DepartmentId
                };

                _departmentUserRepository.InsertOrUpdate(departmentUserMapping);
                _dataService.RefreshDepartmentMapping();
                response = true;
            }
            return response;
        }

        public ListResultDto<DepartmentUserMappingDto> GetUserDepartmentMapping(UserDepartmentServiceInputDto serviceInputDto)
        {
            if(serviceInputDto.DepartmentId == 0 && serviceInputDto.SupervisorId == 0 && serviceInputDto.UserId == 0)
            {
                var data = _departmentUserRepository.GetAll().Select(x => new DepartmentUserMappingDto
                {
                    Id = x.Id,
                    DepartmentId = x.DepartmentId,
                    UserId = x.UserId,
                    SupervisorId = x.SupervisorId
                }).ToList();
                return new ListResultDto<DepartmentUserMappingDto>(data);
            }

            var userDepartmentMapping = _departmentUserRepository.GetAll().Where(x => x.DepartmentId == serviceInputDto.DepartmentId
            || x.UserId == serviceInputDto.UserId
            || x.SupervisorId == serviceInputDto.SupervisorId).Select(x => new DepartmentUserMappingDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                UserId = x.UserId,
                SupervisorId = x.SupervisorId
            }).ToList();
            return new ListResultDto<DepartmentUserMappingDto>(userDepartmentMapping);
        }

        private bool CreateUpdateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var response = false;
                if (departmentDto != null)
                {
                    var entityType = "CreateDepartment";
                    if (departmentDto.Id > 0)
                    {
                        entityType = "UpdateDepartment";
                    }
                    var departmentEntity = new Department
                    {
                        Id = departmentDto.Id,
                        Code = departmentDto.Code,
                        Name = departmentDto.Name,
                        ParentCode = !departmentDto.IsParent ? departmentDto.ParentCode : string.Empty,
                        Description = departmentDto.Description,
                        IsDeleted = departmentDto.IsDeleted
                    };
                    var res = _departmentRepository.InsertOrUpdateAndGetId(departmentEntity);
                    response = true;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

