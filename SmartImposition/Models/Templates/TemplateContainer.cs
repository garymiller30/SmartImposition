using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Input;

namespace SmartImposition.Models.Templates
{
    /// <summary>
    /// темплейт контейнер. 
    /// Зберігаються вхідні файли, темплейт друкарського листа та порядок сторінок
    /// 
    /// </summary>
    public class TemplateContainer
    {
        public Dictionary<int, InputDocument> InputDocuments { get; set; } = new Dictionary<int, InputDocument>();
        public List<TemplatePressSheet> TemplatePressSheets { get; set; } = new List<TemplatePressSheet>(); 
        public TemplateRunList RunList { get; set; } = new TemplateRunList();
    }
}
