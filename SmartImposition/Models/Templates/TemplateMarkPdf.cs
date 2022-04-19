using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Templates
{
    public class TemplateMarkPdf : TemplateMarkAbstract
    {
        public int DocumentId { get; set; }
        public int DocumentPageNo { get; set; } = 1;



    }
}
