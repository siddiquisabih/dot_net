using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global.Project.Model.Exams;
using Global.Project.Model.Options;
using Global.Project.Model.Questions;

namespace Global.Project.SubmitExams.Dto
{
    public class SubmitExamDto
    {
        public int ExamId { get; set; }

        public int QuestionId { get; set; }

        public int OptionId { get; set; }

        public bool IsCorrect { get; set; }

    }
}