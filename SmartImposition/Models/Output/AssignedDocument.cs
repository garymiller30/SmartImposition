using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Output
{
    public class AssignedDocument
    {
        public string FileName { get; set; }
        public InputPage Page { get; set; }
    }
}
