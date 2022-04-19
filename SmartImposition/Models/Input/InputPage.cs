using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Input
{
    /// <summary>
    /// зберігає інформацію про сторінку PDF файла
    /// </summary>
    public class InputPage
    {
        public int PageNo { get; set; }
        public PageFormat Format { get; set; } = new PageFormat();
        public RotateEnum Rotate { get; set; } = RotateEnum._0;

    }
}
