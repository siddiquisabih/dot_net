using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Global.Project.Model.Questions;

namespace Global.Project.Model.Options
{
    public class Option : Entity<int>
    {
        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
