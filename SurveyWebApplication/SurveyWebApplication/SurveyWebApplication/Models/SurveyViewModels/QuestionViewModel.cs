namespace SurveyWebApplication.Models.SurveyViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string TypeCode { get; set; } 
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }

        public int? AnswerMaxLength { get; set; }
        public List<QuestionOptionViewModel> Options { get; set; } = new();
    }
}
