namespace SurveyWebApplication.Models.SurveyViewModels
{
    public class SectionViewModel
    {
        public int SectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDesc { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation flag
        public bool IsSinglePage { get; set; }

        public List<QuestionViewModel> Questions { get; set; } = new();
    }
}
