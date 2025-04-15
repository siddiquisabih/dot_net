using Abp.Domain.Entities;
using Global.Project.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Model
{
    public class UserSignalrConnection : Entity
    {
        public string ConnectionId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Platform { get; set; }
        public DateTime ConnectionStartTime { get; set; } = DateTime.Now;

    }
}
