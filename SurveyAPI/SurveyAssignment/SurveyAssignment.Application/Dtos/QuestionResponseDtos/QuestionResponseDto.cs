using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Application.Dtos.QuestionResponseDtos
{
    public class QuestionResponseDto
    {
        public int QuestionResponseId { get; set; }

        public int QuestionId { get; set; }

        public string? AnswerText { get; set; }

        public int? AnswerOptionId { get; set; }

        public int? RatingValue { get; set; }
    }
}
