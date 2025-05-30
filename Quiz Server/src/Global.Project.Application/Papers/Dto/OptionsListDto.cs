﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Global.Project.Model.Questions;

namespace Global.Project.Papers.Dto
{
    public class OptionsListDto : EntityDto
    {
        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

    }
}
