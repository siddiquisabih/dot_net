using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using System;
using Global.Project.Auditor.Model;
using Global.Project.Auditor;

namespace Global.Project.Controllers
{
    [Route("/api/services/app/[controller]/[action]")]
    public class AuditLogController : ProjectControllerBase
    {
        private readonly IAuditorAppService _auditorAppService;
        public AuditLogController(
            IAuditorAppService auditorAppService)
        {
            _auditorAppService = auditorAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs(AuditLogServiceInputModel serviceInputDto)
        {
            try
            {
                return Ok(await _auditorAppService.GetAuditLogs(serviceInputDto));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
