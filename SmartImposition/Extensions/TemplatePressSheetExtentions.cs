using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Templates;

namespace SmartImposition.Extensions
{
    public static class TemplatePressSheetExtentions
    {
        public static IEnumerable<TemplateMarkPdf> GetMarkPdfs(this TemplatePressSheet sheet)
        {
            var list = new List<TemplateMarkPdf>();

            var marks = sheet.Marks
                .OfType<TemplateMarkPdf>();

            list.AddRange(marks);

            foreach (var pageBlock in sheet.TemplateBlocks)
            {
                list.AddRange(pageBlock.Marks.OfType<TemplateMarkPdf>());
            }

            return list;
        }
    }
}
