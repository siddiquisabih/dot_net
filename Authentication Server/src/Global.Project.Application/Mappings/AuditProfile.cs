using AutoMapper;
using Global.Project.Auditor.Model;
using Global.Project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Mappings
{
    public class AuditProfile: Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditLog, AuditLogModel>()
                .ForMember(d => d.AuditType, s => s.MapFrom(a => a.AuditType.AuditTypeName))
                .ForMember(d => d.UserName, s => s.MapFrom(a => a.User.Name))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.UserId))
                ;

            CreateMap<AuditType, AuditTypeModel>().ReverseMap();
 

        }
    }
}
