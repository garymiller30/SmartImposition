using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
/// <summary>
/// поля навколо об'єкту
/// </summary>
    public class TemplateGutters
    {
        public double Left { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
        /// <summary>
        /// виставити однакові поля для всіх
        /// </summary>
        public double All
        {
            set
            {
                Left = value;
                Right = value;
                Top = value;
                Bottom = value;
            }
        }
    }
}
