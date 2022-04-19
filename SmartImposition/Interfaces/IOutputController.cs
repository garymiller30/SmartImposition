using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Controllers;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface IOutputController
    {
        IOutputDocument CreateDocument(TemplateContainer container, IInputFileController inputFileController, TemplateController templateController);
    }
}
