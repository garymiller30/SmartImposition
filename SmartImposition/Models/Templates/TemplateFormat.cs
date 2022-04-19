using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
    public class TemplateFormat
    {
        public double Width { get; set; }
        public double Height { get; set; }


        public static TemplateFormat operator+ (TemplateFormat format, TemplateGutters gutters)
        {
            var tf = new TemplateFormat
            {
                Width = format.Width+gutters.Left+gutters.Right,
                Height = format.Height+gutters.Bottom+gutters.Top
            };

            return tf;
        }
    }
}
