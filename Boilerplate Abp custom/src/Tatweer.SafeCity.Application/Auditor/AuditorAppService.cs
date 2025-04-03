using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tatweer.SafeCity.Auditor.Model;
using Tatweer.SafeCity.Models;
using Tatweer.SafeCity.Authorization.Users;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Abp.Runtime.Session;

namespace Tatweer.SafeCity.Auditor
{
    public class AuditorAppService : IAuditorAppService
    {
        private readonly IRepository<AuditLog> _auditLogRepository;
        private readonly IRepository<AuditType> _auditTypeRepository;
        private readonly UserManager _userManager;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IAbpSession _abpSession;

        public AuditorAppService(IRepository<AuditType> auditTypeRepository,
            IRepository<AuditLog> auditLogRepository,
            UserManager userManager,
            IConfigurationProvider configurationProvider,
            IAbpSession abpSession
            )
        {
            _auditTypeRepository = auditTypeRepository;
            _auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _configurationProvider = configurationProvider;
            _abpSession = abpSession;
        }

        public bool InsertAuditLog(string auditCode, int userId, int entityId = 0, string entityType = "")
        {
            var response = false;
            var audit = _auditTypeRepository.GetAll();
            var auditType = _auditTypeRepository.GetAll().FirstOrDefault(x => x.AuditTypeCode == auditCode.ToLower());
            if (auditType != null)
            {
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    EntityId = entityId,
                    EntityType = entityType,
                    AuditTypeId = auditType.Id,
                    TimeStamp = DateTime.Now
                };

                _auditLogRepository.InsertAndGetId(auditLog);
                response = true;
            }
            return response;
        }

        public async Task<ListResultDto<AuditLogModel>> GetAuditLogs(AuditLogServiceInputModel serviceInputDto)
        {
            var auditLogDtos = new List<AuditLogModel>();
            var auditType = await _auditTypeRepository.GetAllListAsync();
            var auditLogs = _auditLogRepository.GetAll()
                .Where(x => (serviceInputDto.FromDate.HasValue ? x.TimeStamp >= serviceInputDto.FromDate : true)
            && (serviceInputDto.ToDate.HasValue ? x.TimeStamp <= serviceInputDto.ToDate : true)
            && (serviceInputDto.UserId == 0  ? x.UserId == _abpSession.GetUserId() : x.UserId ==  serviceInputDto.UserId)
            &&(serviceInputDto.TypeId.HasValue ? x.AuditTypeId == serviceInputDto.TypeId: true))
                .ProjectTo<AuditLogModel>(_configurationProvider).ToList();
            auditLogs= auditLogs.OrderByDescending(x=>x.TimeStamp).ToList();

            return new ListResultDto<AuditLogModel>() { Items=auditLogs};
        }

        public async Task<ListResultDto<AuditTypeModel>> GetAuditTypes()
        {
            var types = _auditTypeRepository.GetAll().ProjectTo<AuditTypeModel>(_configurationProvider).ToList();
            return new ListResultDto<AuditTypeModel> { Items = types };
        }
        
    }
}
