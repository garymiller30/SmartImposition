using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Templates
{
    public class TemplatePageBlock
    {
        public int CntXPages { get; set; } = 1;
        public int CntYPages { get; set; } = 1;
        public TemplatePageBlockSettings Settings { get; set; } = new TemplatePageBlockSettings();
        public RotateEnum Rotate { get; set; } = RotateEnum._0;
        public TemplatePosition OffsetPosition { get; set; } = new TemplatePosition();
        public TemplateGutters Gutters { get; set; } = new TemplateGutters();
        public List<TemplatePage> TemplatePages { get; set; } = new List<TemplatePage>();
        public List<ITemplateMark> Marks { get; set; } = new List<ITemplateMark>();
        public TemplatePageBlock(TemplatePageBlockSettings settings)
        {
            Settings = settings;
        }

        public TemplateFormat GetTemplatePageBlockFormat()
        {
            var pageBlockFormat = new TemplateFormat
            {
                Width = Settings.TemplatePageFormat.Width + Settings.TemplatePageGutters.Left + Settings.TemplatePageGutters.Right,
                Height = Settings.TemplatePageFormat.Height + Settings.TemplatePageGutters.Top + Settings.TemplatePageGutters.Bottom
            };
            return pageBlockFormat;
        }

        public TemplateFormat GetPageBlockFormat()
        {

            var w = TemplatePages.Where(x => x.PageIndex.Y == 0)
                .Sum(y => y.GetFormatWithGutters().Width);
            var h = TemplatePages.Where(x => x.PageIndex.X == 0)
                .Sum(y => y.GetFormatWithGutters().Height);


            if (Rotate == RotateEnum._0 || Rotate == RotateEnum._180)
            {
                return new TemplateFormat
                {
                    Width = w,
                    Height = h

                };
            }

            return new TemplateFormat
            {
                Height = w,
                Width = h
            };
        }

    }
}
