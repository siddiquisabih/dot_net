using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Options.Dto;

namespace Global.Project.Options
{
    public interface IOptionAppService : IApplicationService
    {

        Task<int> CreateNewOption(OptionDto body);

        Task<List<OptionDto>> GetAllOptions();
    
    }
}
