using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Global.Project.Questions.Dto
{
    public class QuestionDto : EntityDto
    {


        public string Title { get; set; }


        public int ExamId { get; set; }


    }
}
