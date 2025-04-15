using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Global.Project.Exams.Dto;
using Global.Project.Model.Exams;

namespace Global.Project.Exams
{
    public class ExamAppService : ApplicationService, IExamService
    {

        private readonly IRepository<Exam> _examRepo;

        public ExamAppService(IRepository<Exam> examRepo)
        {
            _examRepo = examRepo;
        }


        public Task<int> CreateNewExam(ExamDto body)
        {

            if (string.IsNullOrWhiteSpace(body.Title))
            {
                throw new Abp.UI.UserFriendlyException("Title is required");
            }

            var exam = ObjectMapper.Map<Exam>(body);
            return _examRepo.InsertAndGetIdAsync(exam);
        }

        public async Task<List<ExamDto>> GetAllExamList()
        {
            var list = await _examRepo.GetAllListAsync();
            return ObjectMapper.Map<List<ExamDto>>(list);
        }
    }
}
