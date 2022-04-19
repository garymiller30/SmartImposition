using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Input;
using SmartImposition.Models.Templates;

namespace SmartImposition.Interfaces
{
    public interface IInputFileController
    {
        int Load(string file);
        InputDocument GetDocument(int id);
    }
}
