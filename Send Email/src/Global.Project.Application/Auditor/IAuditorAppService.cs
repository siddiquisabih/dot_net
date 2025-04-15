using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Global.Project.Auditor.Model;
using System.Threading.Tasks;

namespace Global.Project.Auditor
{
    public interface IAuditorAppService : IApplicationService
    {
        bool InsertAuditLog(string auditCode, int userId, int entityId = 0, string entityType = "");
        Task<ListResultDto<AuditLogModel>> GetAuditLogs(AuditLogServiceInputModel serviceInputDto);
    }
}
