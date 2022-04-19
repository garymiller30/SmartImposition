using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Extensions;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    /// <summary>
    /// керування темплейтами
    /// створює TemplatePressSheet,TemplatePageBlock
    /// </summary>
    public class TemplateController
    {
        //public List<TemplatePressSheet> TemplatePressSheets { get; set; } = new List<TemplatePressSheet>();
        private TemplateContainer _container;
        public TemplateController(TemplateContainer container)
        {
            _container = container;
        }

        public TemplatePressSheet CreateTemplateSheet(TemplatePressSheetSettings templatePressSheetSettings)
        {
            var ps = new TemplatePressSheet(templatePressSheetSettings);
            _container.TemplatePressSheets.Add(ps);

            return ps;
        }

        public TemplatePageBlock CreateTemplatePageBlock(TemplatePageBlockSettings templatePageBlockSettings)
        {
            var pb = new TemplatePageBlock(templatePageBlockSettings);
            return pb;
        }

        public int GetCountPressSheets()
        {
            return _container.TemplatePressSheets.Count;
        }


        public IEnumerable<TemplateMarkPdf> GetMarkPdfs()
        {
            List<TemplateMarkPdf> marks = new List<TemplateMarkPdf>();

            foreach (var sheet in _container.TemplatePressSheets)
            {
                marks.AddRange(sheet.GetMarkPdfs());
            }

            return marks;
        }
    }
}
