using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
    public class TemplateLineMark : TemplateMarkAbstract
    {
        public double Lenght { get; set; } = 5;
        public double Width { get; set; } = 0.2d;
        public TemplateColorMark Color { get; set; } = new TemplateColorMark();
    }
}
