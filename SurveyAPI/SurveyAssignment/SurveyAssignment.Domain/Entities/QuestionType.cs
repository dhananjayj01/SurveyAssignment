using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }

        public string TypeCode { get; set; } = null!;   // SINGLE, MULTI, TEXT, RATING
        public string TypeName { get; set; } = null!;   // Single Select, Multi Select etc.

        /* Audit fields */
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        /* Navigation */
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
