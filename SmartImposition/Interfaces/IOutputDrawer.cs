using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartImposition.Interfaces
{
    public interface IOutputDrawer
    {
        void Save(string outputFileName, IOutputDocument document);
    }
}
