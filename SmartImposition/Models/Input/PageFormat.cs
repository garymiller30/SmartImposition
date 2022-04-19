using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Input
{
    public class PageFormat
    {
        public PageBox MediaBox { get; set; } = new PageBox();
        public PageBox TrimBox { get; set; } = new PageBox();
        /// <summary>
        /// Reserved. Not used
        /// </summary>
        public PageBox CropBox { get; set; } = new PageBox();
        /// <summary>
        /// Reserved. Not used
        /// </summary>
        public PageBox BleedBox { get; set; } = new PageBox();
    }
}
