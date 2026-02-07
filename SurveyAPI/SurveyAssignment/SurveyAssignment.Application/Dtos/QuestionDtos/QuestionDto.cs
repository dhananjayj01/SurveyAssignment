using SurveyAssignment.Application.Dtos.QuestionOptionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Application.Dtos.QuestionDtos
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
        public int? AnswerMaxLength { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
