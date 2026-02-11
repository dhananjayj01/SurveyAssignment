using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Application.Dtos.QuestionOptionDtos
{
    public class QuestionOptionDto
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public int DisplayOrder { get; set; }
    }
}
