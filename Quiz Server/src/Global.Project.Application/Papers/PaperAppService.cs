using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper.Internal.Mappers;
using Global.Project.Model.Exams;
using Global.Project.Model.Options;
using Global.Project.Model.Questions;
using Global.Project.Papers;
using Global.Project.Papers.Dto;

namespace Global.Project.Paper
{
    public class PaperAppService : ApplicationService, IPaperAppService
    {
        private readonly IRepository<Exam> _examRepo;
        private readonly IRepository<Question> _questionRepo;
        private readonly IRepository<Option> _optionRepo;

        public PaperAppService(IRepository<Exam> examRepo, IRepository<Question> questionRepo, IRepository<Option> optionRepo)
        {
            _examRepo = examRepo;
            _questionRepo = questionRepo;
            _optionRepo = optionRepo;
        }



        public async Task<PaperDto> GetPaperByExamId(int examId)
        {

            var getExam = await _examRepo.GetAsync(examId);
            var getQuestions = await _questionRepo.GetAllListAsync(x => x.ExamId == examId);

            var questionIds = getQuestions.Select(q => q.Id).ToList();
            var options = await _optionRepo.GetAllListAsync(o => questionIds.Contains(o.QuestionId));

            foreach (var question in getQuestions)
            {
                question.Options = options.Where(o => o.QuestionId == question.Id).ToList();
            }

            var paper = new PaperDto();
            paper.ExamId = getExam.Id;
            paper.ExamTitle = getExam.Title;
            paper.QuestionsList = ObjectMapper.Map<List<QuestionListDto>>(getQuestions);

            return paper;
        }
    }
}


//var getExam = await _examRepo.GetAsync(examId);
//var getQuestions = await _questionRepo.GetAllListAsync(x => x.ExamId == examId);


//var paper = new PaperDto();

//paper.ExamId = getExam.Id;
//paper.ExamTitle = getExam.Title;


//paper.QuestionsList = ObjectMapper.Map<List<QuestionListDto>>(getQuestions);

//return paper;
