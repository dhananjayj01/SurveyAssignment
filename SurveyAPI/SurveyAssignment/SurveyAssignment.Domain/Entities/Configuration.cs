using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAssignment.Domain.Entities
{
    public class Configuration
    {
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}
