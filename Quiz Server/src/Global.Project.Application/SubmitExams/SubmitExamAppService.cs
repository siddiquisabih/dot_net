using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Global.Project.Model.Options;
using Global.Project.Model.SubmitExams;
using Global.Project.Options.Dto;
using Global.Project.SubmitExams.Dto;

namespace Global.Project.SubmitExams
{
    public class SubmitExamAppService : ApplicationService, ISubmitExamAppService
    {

        private readonly IRepository<SubmitExam> _submitRepo;
        public SubmitExamAppService(IRepository<SubmitExam> submitExam)
        {
            _submitRepo = submitExam;
        }



        public async Task<List<SubmitExamDto>> GetAllSubmittedExams()
        {
            var list = await _submitRepo.GetAllListAsync();
            return ObjectMapper.Map<List<SubmitExamDto>>(list);
        }

        public async Task<int> SubmitNewExam(SubmitExamDto body)
        {
            var option = ObjectMapper.Map<SubmitExam>(body);
            return await _submitRepo.InsertAndGetIdAsync(option);
        }
    }
}
