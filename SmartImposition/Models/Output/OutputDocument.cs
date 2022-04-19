using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;

namespace SmartImposition.Models.Output
{
    public class OutputDocument : IOutputDocument
    {
        public List<OutputPage> Pages { get; set; } = new List<OutputPage>();

        public void Add(OutputPage page)
        {
            Pages.Add(page);
        }
    }
}
