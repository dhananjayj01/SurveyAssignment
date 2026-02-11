using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class QuestionResponse
    {
        public int QuestionResponseId { get; set; }

        public int ResponseId { get; set; }

        public int QuestionId { get; set; }

        public string? AnswerText { get; set; }

        public int? AnswerOptionId { get; set; }

        public int? RatingValue { get; set; }

        // Navigation
        public SurveyResponse SurveyResponse { get; set; }
    }
}
