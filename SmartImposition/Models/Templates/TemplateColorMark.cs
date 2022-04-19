using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
    public class TemplateColorMark
    {
        public bool IsSpot { get; set; }
        public string Name { get; set; }
        public double Cyan { get; set; }
        public double Magenta { get; set; }
        public double Yellow { get; set; }
        public double Black { get; set; }

    }
}
