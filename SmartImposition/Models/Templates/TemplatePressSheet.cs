using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Templates
{
    public class TemplatePressSheet
    {
        public TemplatePressSheetSettings Settings { get; set; }
        public List<TemplatePageBlock> TemplateBlocks { get; set; } = new List<TemplatePageBlock>();
        public List<ITemplateMark> Marks { get; set; } = new List<ITemplateMark>();

        public TemplatePressSheet(TemplatePressSheetSettings settings)
        {
            Settings = settings;
        }

        public TemplatePressSheet()
        {
            
        }

        public TemplateFormat GetPrintableFormat()
        {
            var printableFormat = new TemplateFormat
            {
                Width =
                    Settings.Format.Width - (Settings.NonPrintableGutters.Left + Settings.NonPrintableGutters.Right),
                Height = Settings.Format.Height -
                         (Settings.NonPrintableGutters.Top + Settings.NonPrintableGutters.Bottom)
            };
            return printableFormat;
        }

        public int GetPrintSidesCount()
        {
            if (Settings.PrintType == TemplatePressSheetPrintTypeEnum.SheetWise) return 2;
            return 1;
        }

        public TemplatePageBlock AddPageBlock(TemplatePageBlockSettings setting)
        {
            var block = new TemplatePageBlock(setting);
            TemplateBlocks.Add(block);
            return block;
        }
    }
}
