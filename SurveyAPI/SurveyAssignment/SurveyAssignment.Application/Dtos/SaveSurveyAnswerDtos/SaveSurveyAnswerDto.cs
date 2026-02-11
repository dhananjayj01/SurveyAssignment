using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Application.Dtos.SaveSurveyAnswerDtos
{
    public class SaveSurveyAnswerDto
    {
        public int SurveyId { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public string? AnswerText { get; set; }

        public int? AnswerOptionId { get; set; }

        public int? RatingValue { get; set; }

        public int CreatedBy { get; set; }
    }
}
