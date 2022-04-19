using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface IMarkController
    {
        ITemplateMark AddPdf(string pdf);
        void CreateCutMarks(TemplatePageBlock pageBlock,double lenght, double width);
    }
}
