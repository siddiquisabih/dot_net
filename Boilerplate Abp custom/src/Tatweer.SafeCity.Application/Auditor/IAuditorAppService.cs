using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using Tatweer.SafeCity.Auditor.Model;

namespace Tatweer.SafeCity.Auditor
{
    public interface IAuditorAppService : IApplicationService
    {
        bool InsertAuditLog(string auditCode, int userId, int entityId = 0, string entityType = "");
        Task<ListResultDto<AuditLogModel>> GetAuditLogs(AuditLogServiceInputModel serviceInputDto);
    }
}
