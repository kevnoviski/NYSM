using AutoMapper;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Profiles
{
    public class QuestionProfile :Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionCreateAlterDto,Question>();
            CreateMap<Question,QuestionCreateAlterDto>();
        }
    }
}