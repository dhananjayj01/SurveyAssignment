namespace SurveyWebApplication.Models.SurveyViewModels
{
    public class QuestionOptionViewModel
    {
        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        public string OptionText { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
