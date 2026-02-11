using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class QuestionOption
    {
        public int OptionId { get; set; }

        public int QuestionId { get; set; }
        public string OptionText { get; set; } = null!;
        public int DisplayOrder { get; set; }

        /* Audit fields */
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        /* Navigation */
        public Question Question { get; set; } = null!;
    }
}

