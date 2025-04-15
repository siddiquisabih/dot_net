using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Global.Project.Papers.Dto
{
    public class PaperDto : EntityDto
    {

        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public List<QuestionListDto> QuestionsList { get; set; }


    }
}
