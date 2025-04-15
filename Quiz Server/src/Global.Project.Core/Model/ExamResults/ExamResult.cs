using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Global.Project.Model.Exams;

namespace Global.Project.Model.ExamResults
{
    public class ExamResult : Entity<int>
    {

        public float Score { get; set; }

        public string Grade { get; set; }


        public int ExamId { get; set; }

        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }



    }
}
