using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class Section
    {
        public int SectionId { get; set; }
        public int SurveyId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDesc { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsSinglePage { get; set; }
        public bool IsDeleted { get; set; }
        public Survey Survey { get; set; }
        public ICollection<Question> Question { get; set; } = new List<Question>();
    }

}
