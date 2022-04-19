using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Templates
{
    public class TemplateMarkAbstract : ITemplateMark
    {
        public TemplatePosition OffsetPosition { get; set; } = new TemplatePosition();
        public TemplateFormat Format { get; set; } = new TemplateFormat();
        public AnchorPointEnum MarkAnchor { get; set; } = AnchorPointEnum.Center;
        public AnchorPointEnum SubjectAnchor { get; set; } = AnchorPointEnum.Center;
        public RotateEnum Rotate { get; set; } = RotateEnum._0;
        public PageBox ClipBox { get; set; } = new PageBox();
        public TemplateSidePosition SidePosition { get; set; } = new TemplateSidePosition();
        public bool Foreground { get; set; }
    }
}
