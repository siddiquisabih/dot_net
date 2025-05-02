using Abp.Domain.Entities;
using Global.Project.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Global.Project.Model
{
    public class AuditLog : Entity
    {
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public DateTime TimeStamp { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public int AuditTypeId { get; set; }
        public AuditType AuditType { get; set; }
    }
}
