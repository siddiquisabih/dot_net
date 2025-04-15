using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Global.Project.Model.Exams;
using Global.Project.Model.Options;

namespace Global.Project.Papers.Dto
{
    public class QuestionListDto : EntityDto
    {

        public string Title { get; set; }

        public int ExamId { get; set; }

        public List<OptionsListDto> OptionsList { get; set; }

    }
}
