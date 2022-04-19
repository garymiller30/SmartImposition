using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface IOutputMark
    {
         bool Foreground { get; set; }
         TemplatePosition Position { get; set; }
         TemplateFormat Format { get; set; }
    }
}
