using System;
using System.Collections.Generic;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Templates
{
    public class TemplatePage
    {
        public TemplatePageIndex PageIndex { get; set; }

        public TemplateNumeration Numeration { get; set; } = new TemplateNumeration();
        public bool Rotated { get; set; } = false;
        public TemplatePosition Position { get; set; } = new TemplatePosition();
        public TemplateFormat Format { get; set; } = new TemplateFormat();
        public TemplateGutters Gutters { get; set; } = new TemplateGutters();
        public List<TemplateLineMark> CutMarks { get; set; } = new List<TemplateLineMark>();
        public TemplateFormat GetFormatWithGutters()
        {
            return new TemplateFormat
            {
                Width = Format.Width + Gutters.Left + Gutters.Right,
                Height = Format.Height + Gutters.Bottom + Gutters.Top
            };

        }

    }
}
