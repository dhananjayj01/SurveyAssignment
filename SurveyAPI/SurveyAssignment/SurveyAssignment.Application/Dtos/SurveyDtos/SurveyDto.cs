//using SurveyAssignment.Application.Dtos.SectionDtos; ///pre
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SurveyAssignment.Application.Dtos.SurveyDtos
//{
//    public class SurveyDto
//    {
//        public int SurveyId { get; set; }
//        public string SurveyName { get; set; }
//        public string SurveyDesc { get; set; }
//        public List<SectionDto> Sections { get; set; }
//    }
//}


using SurveyAssignment.Application.Dtos.ConfigurationDtos;
using SurveyAssignment.Application.Dtos.SectionDtos;
namespace SurveyAssignment.Application.Dtos.SurveyDtos
{

    public class SurveyDto
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDesc { get; set; }


        public ConfigurationDto RatingConfig { get; set; }
        public List<SectionDto> Sections { get; set; } = new();
    }
}