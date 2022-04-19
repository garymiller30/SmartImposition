using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Output
{
    public class OutputPdfMark : OutputMarkAbstract
    {
       public string File { get; set; }
       public int PageNo { get; set; }
       public RotateEnum Rotate { get; set; }
       public AnchorPointEnum MarkAnchor { get; set; }
    }
}
