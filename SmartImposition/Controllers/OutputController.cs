using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Output;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class OutputController
    {
        private IOutputController _controller;
        private TemplateContainer _container;
        public OutputController(TemplateContainer container)
        {
            _container = container;

        }
        public IOutputDocument CreateOutputDocument(TemplateContainer container, IOutputController outputController, IInputFileController inputFileController,TemplateController templateController)
        {
            _controller = outputController;

            return outputController.CreateDocument(container,inputFileController, templateController);
        }

    }
}
