using System.Text.Json.Serialization;

namespace SurveyWebApplication.Models.SurveyViewModels
{
    public class SurveyViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDesc { get; set; }

        public int CurrentSectionIndex { get; set; } = 0;
        public int CurrentQuestionIndex { get; set; } = 0;


        public List<SectionViewModel> Sections { get; set; } = new();
        [JsonPropertyName("ratingConfig")]
        public RatingConfigurationViewModel RatingConfig{ get; set; }
    }
}
