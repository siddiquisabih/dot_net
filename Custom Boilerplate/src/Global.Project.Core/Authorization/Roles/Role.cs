
using Abp.Authorization.Roles;
using Global.Project.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Global.Project.Authorization.Roles
{
    public class Role : AbpRole<User>
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public   override int Id { get; set; }
        public const int MaxDescriptionLength = 5000;

        public Role()
        {
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }

        [StringLength(MaxDescriptionLength)]
        public string Description {get; set;}

    }
}
