using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models
{
    public class ImpositionSettings
    {
        public BindingTypeEnum BindingType { get; set; } = BindingTypeEnum.None;
    }
}
