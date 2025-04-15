using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Global.Project.Model.Exams;
using Global.Project.Model.Questions;
using Global.Project.Questions.Dto;

namespace Global.Project.Questions
{
    public class QuestionAppService : ApplicationService, IQuestionAppService
    {
        private readonly IRepository<Question> _questioRepo;
        public QuestionAppService(IRepository<Question> questioRepo)
        {
            _questioRepo = questioRepo;
        }

        public async Task<int> CreateNewQuestion(QuestionDto body)
        {

            if (string.IsNullOrWhiteSpace(body.Title))
            {
                throw new Abp.UI.UserFriendlyException("Title is required");
            }

            if (body.ExamId <= 0)
            {
                throw new Abp.UI.UserFriendlyException("Valid Exam ID is required");
            }
            var question = ObjectMapper.Map<Question>(body);
            return await _questioRepo.InsertAndGetIdAsync(question);
        }

        public async Task<List<QuestionDto>> GetAllQuestions()
        {
            var listQuestion = await _questioRepo.GetAllAsync();
            return ObjectMapper.Map<List<QuestionDto>>(listQuestion);
        }
    }
}