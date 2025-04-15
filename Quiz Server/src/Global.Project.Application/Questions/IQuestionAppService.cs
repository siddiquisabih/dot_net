using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Questions.Dto;

namespace Global.Project.Questions
{
    public interface IQuestionAppService : IApplicationService
    {


        Task<int> CreateNewQuestion(QuestionDto body);

        Task<List<QuestionDto>> GetAllQuestions();

    }
}
