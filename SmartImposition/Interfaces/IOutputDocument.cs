﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Output;

namespace SmartImposition.Interfaces
{
    public interface IOutputDocument
    {
        List<OutputPage> Pages { get; set; }
    }
}
