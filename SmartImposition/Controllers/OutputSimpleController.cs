using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Handbook;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Output;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class OutputSimpleController : IOutputController
    {
        private TemplateContainer _container;

        public IOutputDocument CreateDocument(TemplateContainer container, IInputFileController inputFileController, TemplateController templateController)
        {
            _container = container;
            var doc = new OutputDocument();

            var cntPressSheets = templateController.GetCountPressSheets();
           
            for (int i = 0; i < cntPressSheets; i++)
            {
                var pressSheet = _container.TemplatePressSheets[i];

                
                switch (pressSheet.Settings.PrintType)
                {
                    case TemplatePressSheetPrintTypeEnum.SingleSide:

                        var drawFront = new OutputDrawFront(_container,inputFileController,pressSheet);
                        OutputPage page = drawFront.Draw();
                        doc.Add(page);

                        break;
                    case TemplatePressSheetPrintTypeEnum.SheetWise:

                        OutputPage front =  new OutputDrawFront(_container,inputFileController,pressSheet).Draw();
                        OutputPage back = new OutputDrawBack(_container, inputFileController,pressSheet).Draw();

                        doc.Add(front);
                        doc.Add(back);

                        break;
                    case TemplatePressSheetPrintTypeEnum.WorkAndTurn:

                        var draw = new OutputDrawWorkAndTurn(_container,inputFileController,pressSheet);
                        OutputPage p = draw.Draw();
                        doc.Add(p);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return doc;
        }
    }
}
