using AutoMapper;
using SurveyAssignment.Application.Dtos.ConfigurationDtos;
using SurveyAssignment.Application.Dtos.QuestionDtos;
using SurveyAssignment.Application.Dtos.QuestionOptionDtos;
using SurveyAssignment.Application.Dtos.SectionDtos;
using SurveyAssignment.Application.Dtos.SurveyDtos;
using SurveyAssignment.Domain.Entities;

namespace SurveyAssignment.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Survey, SurveyDto>();
            CreateMap<Section, SectionDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionOption, QuestionOptionDto>();
            CreateMap<Configuration, ConfigurationDto>();
            CreateMap<QuestionType, QuestionDto>()
                .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeName));
        }
    }
}
