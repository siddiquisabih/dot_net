using Abp.Application.Services.Dto;

namespace Global.Project.Departments.Dto
{
    public class DepartmentDto : EntityDto
    {
        public DepartmentDto(
            int id,
            string code,
            string name,
            string description,
            string parentCode,
            bool isParent,
            bool isDeleted) 
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            ParentCode = parentCode;
            IsParent = isParent;
            IsDeleted = isDeleted;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCode { get; set; }
        public bool IsParent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
