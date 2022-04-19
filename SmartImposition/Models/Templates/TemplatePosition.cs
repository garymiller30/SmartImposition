using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
    public class TemplatePosition
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"X={X:N1}, Y={Y:N1}";
        }

        public static TemplatePosition operator+ (TemplatePosition a, TemplatePosition b)
        {
            var tp = new TemplatePosition
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
            return tp;
        }

        public static TemplatePosition operator- (TemplatePosition a, TemplatePosition b)
        {
            var tp = new TemplatePosition
            {
                X = a.X - b.X,
                Y = a.Y - b.Y
            };
            return tp;
        }
    }
}
