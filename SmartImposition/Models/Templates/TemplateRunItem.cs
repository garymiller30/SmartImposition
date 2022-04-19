using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Models.Templates
{
    public class TemplateRunItem
    {
        public int PageIdx { get; set; }
        public int InputFileId { get; set; }
        public int InputFilePageNo { get; set; }
    }
}
