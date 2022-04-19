using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Templates
{
    public class TemplatePressSheetSettings
    {
        public TemplatePressSheetPrintTypeEnum PrintType { get; set; } = TemplatePressSheetPrintTypeEnum.SingleSide;
        public TemplateFormat Format { get; set; } = new TemplateFormat();
        public TemplateGutters NonPrintableGutters { get; set; } = new TemplateGutters();
        /// <summary>
        /// відносно якої точки розміщати сторінки
        /// </summary>
        public AnchorPointEnum PlaceAnchorPoint { get; set; } = AnchorPointEnum.BottomCenter;
    }
}
