using Abp.Domain.Entities;

namespace Global.Project.Lookups
{
    public class Region : Entity
    {

        public string Name { get; set; }

        public bool IsActive { get; set; }

    }
}
