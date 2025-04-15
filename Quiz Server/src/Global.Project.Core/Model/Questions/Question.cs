using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Global.Project.Model.Exams;
using Global.Project.Model.Options;

namespace Global.Project.Model.Questions
{
    public class Question : Entity<int>
    {
        public Question()
        {
            Options = new List<Option>();
        }

        public string Title { get; set; }

        public int ExamId { get; set; }

        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }

        public virtual ICollection<Option> Options { get; set; }



    }
}
