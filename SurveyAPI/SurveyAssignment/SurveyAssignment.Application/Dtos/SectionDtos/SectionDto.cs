using SurveyAssignment.Application.Dtos.QuestionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Application.Dtos.SectionDtos
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDesc { get; set; }
        public bool IsSinglePage { get; set; }
        public int DisplayOrder { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
