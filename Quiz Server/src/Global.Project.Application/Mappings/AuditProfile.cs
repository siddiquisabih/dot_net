using AutoMapper;
using Global.Project.Auditor.Model;
using Global.Project.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Global.Project.Model.Questions;
using Global.Project.Model.Options;
using Global.Project.Exams.Dto;
using Global.Project.Model.Exams;
using Global.Project.Questions.Dto;
using Global.Project.Options.Dto;
using Global.Project.SubmitExams.Dto;
using Global.Project.Model.SubmitExams;
using Global.Project.Papers.Dto;

namespace Global.Project.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditLog, AuditLogModel>()
                .ForMember(d => d.AuditType, s => s.MapFrom(a => a.AuditType.AuditTypeName))
                .ForMember(d => d.UserName, s => s.MapFrom(a => a.User.Name))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.UserId))
                ;

            CreateMap<AuditType, AuditTypeModel>().ReverseMap();

            CreateMap<ExamDto, Exam>().ReverseMap();

            CreateMap<QuestionDto, Question>().ReverseMap();
            CreateMap<QuestionListDto, Question>().ReverseMap();


            CreateMap<OptionDto, Option>().ReverseMap();
            CreateMap<OptionsListDto, Option>().ReverseMap();

            CreateMap<SubmitExamDto, SubmitExam>().ReverseMap();



        }
    }
}
