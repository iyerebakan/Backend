﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Abstraction
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}