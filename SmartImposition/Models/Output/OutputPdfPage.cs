using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Output
{
    public class OutputPdfPage
    {
        public TemplatePosition Position { get; set; } = new TemplatePosition();
        public TemplateFormat Format { get; set; } = new TemplateFormat();
        public TemplateGutters Gutters { get; set; } = new TemplateGutters();
        public RotateEnum Rotate { get; set; }
        public AssignedDocument AssignedDocument { get; set; } = new AssignedDocument();
        public PageBox ClipBox { get; set; } = new PageBox();
        public PageBox TrimBox { get; set; } = new PageBox();

        public List<ITemplateMark> Marks { get; set; } = new List<ITemplateMark>();

    }
}
