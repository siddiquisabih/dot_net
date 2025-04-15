using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Global.Project.Exams.Dto;
using Global.Project.Model.Options;
using Global.Project.Model.Questions;
using Global.Project.Options.Dto;

namespace Global.Project.Options
{
    public class OptionAppService : ApplicationService, IOptionAppService
    {

        private readonly IRepository<Option> _optionRepo;
        public OptionAppService(IRepository<Option> optionRepo)
        {
            _optionRepo = optionRepo;
        }


        public async Task<int> CreateNewOption(OptionDto body)
        {
            if (string.IsNullOrWhiteSpace(body.Title))
            {
                throw new Abp.UI.UserFriendlyException("Title is required");
            }

            if (body.QuestionId <= 0)
            {
                throw new Abp.UI.UserFriendlyException("Valid Question ID is required");
            }
            var option = ObjectMapper.Map<Option>(body);
            return await _optionRepo.InsertAndGetIdAsync(option);
        }

        public async Task<List<OptionDto>> GetAllOptions()
        {
            var list = await _optionRepo.GetAllListAsync();
            return ObjectMapper.Map<List<OptionDto>>(list);
        }


    }
}
