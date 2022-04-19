using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class AutomateSimple : IImposer
    {
        public void PlaceMaximum(ImpositionSettings settings,TemplatePressSheet templateSheet,TemplatePageBlock pageBlock)
        {
            switch (settings.BindingType)
            {
                case BindingTypeEnum.None:

                    new AutomateSimpleNone().Place(templateSheet,pageBlock);

                    break;
                case BindingTypeEnum.SaddleStitch:
                    break;
                case BindingTypeEnum.Binder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Impose(ImpositionSettings impositionSettings, TemplatePressSheet templateSheet, TemplatePageBlock pageBlock)
        {
            PlaceMaximum(impositionSettings,templateSheet,pageBlock);
        }
    }
}
