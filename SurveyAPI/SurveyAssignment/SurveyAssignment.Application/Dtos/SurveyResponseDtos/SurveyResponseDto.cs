using SurveyAssignment.Application.Dtos.QuestionResponseDtos;

namespace SurveyAssignment.Application.Dtos.SurveyResponseDtos
{
    public class SurveyResponseDto
    {
        public int ResponseId { get; set; }

        public int SurveyId { get; set; }

        public int UserId { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public List<QuestionResponseDto> Answers { get; set; }
            = new();
    }
}
