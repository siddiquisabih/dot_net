using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.SubmitExams.Dto;

namespace Global.Project.SubmitExams
{
    public interface ISubmitExamAppService : IApplicationService
    {


        Task<int> SubmitNewExam(SubmitExamDto body);

        Task<List<SubmitExamDto>> GetAllSubmittedExams();

    }
}
