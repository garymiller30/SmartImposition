using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Output
{
    public class OutputPage
    {
        public PageBox Format { get; set; } = new PageBox();
        public List<OutputPdfPage> PdfPages { get; set; } = new List<OutputPdfPage>();

        public List<IOutputMark> Marks { get; set; } = new List<IOutputMark>();
        
    }
}
