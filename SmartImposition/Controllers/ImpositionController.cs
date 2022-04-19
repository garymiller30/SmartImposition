using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Handbook;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class ImpositionController
    {
        public ImpositionAutomate Automate { get; } = new ImpositionAutomate();
        private TemplateContainer _container;
        public ImpositionController(TemplateContainer container)
        {
            _container = container;
        }

      
    }
}
