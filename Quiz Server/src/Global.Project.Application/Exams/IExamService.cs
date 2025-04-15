using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Exams.Dto;

namespace Global.Project.Exams
{
    public interface IExamService : IApplicationService
    {

        Task<int> CreateNewExam(ExamDto body);

        Task<List<ExamDto>> GetAllExamList();

    }
}
