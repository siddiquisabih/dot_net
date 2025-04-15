using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Global.Project.Model.Questions;

namespace Global.Project.Model.Exams
{
    public class Exam : Entity<int>
    {

        public string Title  { get; set; }


    }
}
