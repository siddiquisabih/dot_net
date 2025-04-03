using Abp.Application.Services.Dto;

namespace Tatweer.SafeCity.Departments.Dto
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
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.ParentCode = parentCode;
            this.IsParent = isParent;
            this.IsDeleted = isDeleted;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCode { get; set; }
        public bool IsParent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
