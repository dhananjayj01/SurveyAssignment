using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int SectionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
        public int? AnswerMaxLength { get; set; }
        public QuestionType QuestionType { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public Section Section { get; set; }
        public ICollection<QuestionOption> Option { get; set; } = new List<QuestionOption>();
    }
}
