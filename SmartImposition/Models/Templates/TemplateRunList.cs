using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Templates
{
    public class TemplateRunList
    {
        public List<TemplateRunItem> TemplateRunPages { get; set; } = new List<TemplateRunItem>();


        public void Add(int inputFileId, int inputPageNo)
        {

            int id;

            if (TemplateRunPages.Any())
            {
                id = TemplateRunPages.Max(x => x.PageIdx) + 1;
            }
            else
            {
                id = 1;
            }

            

            var runItem = new TemplateRunItem
            {
                PageIdx = id,
                InputFileId = inputFileId,
                InputFilePageNo = inputPageNo

            };

            TemplateRunPages.Add(runItem);
        }

        public TemplateRunItem GetRunItem(int pageNo)
        {
            return TemplateRunPages.Find(x => x.PageIdx == pageNo);
        }
    }
}
