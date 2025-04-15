using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Global.Project.Exams.Dto
{
    public class CreateExamDto : EntityDto
    {
        public string Title { get; set; }

    }
}
