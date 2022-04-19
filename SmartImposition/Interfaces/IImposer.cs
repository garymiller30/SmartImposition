using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface IImposer
    {
        void Impose(ImpositionSettings impositionSettings, TemplatePressSheet templateSheet, TemplatePageBlock pageBlock);
    }
}
