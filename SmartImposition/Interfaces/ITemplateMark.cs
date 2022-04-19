using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface ITemplateMark
    {
        TemplatePosition OffsetPosition { get; set; }
        AnchorPointEnum MarkAnchor { get; set; }
        AnchorPointEnum SubjectAnchor { get; set; }
        RotateEnum Rotate { get; set; }
        PageBox ClipBox { get; set; }
        TemplateSidePosition SidePosition { get; set; }
        bool Foreground { get; set; }
        TemplateFormat Format { get; set; }
    }
}
