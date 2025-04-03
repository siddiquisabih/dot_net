using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Abp.Domain.Repositories;
using Tatweer.SafeCity.Models;
using Abp.Application.Services.Dto;
using Tatweer.SafeCity.Auditor;
using Tatweer.SafeCity.Auditor.Model;
using System.Threading.Tasks;
using System;

namespace Tatweer.SafeCity.Controllers
{
    [Route("/api/services/app/[controller]/[action]")]
    public class AuditLogController : SafeCityControllerBase
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
