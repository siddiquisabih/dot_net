using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Global.Project.Model.Exams;
using Global.Project.Model.Options;
using Global.Project.Model.Questions;

namespace Global.Project.Model.SubmitExams
{
    public class SubmitExam : Entity<int>
    {

        
        public int ExamId { get; set; }

        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        public int OptionId { get; set; }

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; }

        public bool IsCorrect { get; set; }

    }
}
