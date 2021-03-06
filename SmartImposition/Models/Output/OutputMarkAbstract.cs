using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Output
{
    public class OutputMarkAbstract : IOutputMark
    {
        public bool Foreground { get; set; }

        public TemplatePosition Position { get; set; } = new TemplatePosition();
        public TemplateFormat Format { get; set; } = new TemplateFormat();
    }
}
