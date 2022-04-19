using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Templates
{
    public class TemplatePageBlockSettings
    {
        public bool UseFormatFromFile { get; set; }
        public TemplateFormat TemplatePageFormat { get; set; } = new TemplateFormat();
        public TemplateGutters TemplatePageGutters { get; set; } = new TemplateGutters();

        public AnchorPointEnum AnchorPoint { get; set; } = AnchorPointEnum.BottomCenter;
    }
}
