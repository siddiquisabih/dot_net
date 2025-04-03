
using Abp.Authorization.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tatweer.SafeCity.Authorization.Users;

namespace Tatweer.SafeCity.Authorization.Roles
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

       // public int RolePermanantId { get; set; }
    }
}
