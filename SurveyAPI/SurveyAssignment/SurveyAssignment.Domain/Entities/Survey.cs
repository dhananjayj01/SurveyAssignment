using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Section> Section { get; set; } = new List<Section>();
    }
}
