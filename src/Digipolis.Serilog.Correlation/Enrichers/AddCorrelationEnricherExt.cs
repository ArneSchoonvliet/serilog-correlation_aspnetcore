﻿using System;
using System.Linq;
using Digipolis.Serilog.Enrichers;

namespace Digipolis.Serilog
{
    public static class AddCorrelationEnricherExt
    {
        public static SerilogExtensionsOptions AddCorrelationEnricher(this SerilogExtensionsOptions options)
        {
            if ( !options.EnricherTypes.Contains(typeof(CorrelationEnricher)) )
                options.AddEnricher<CorrelationEnricher>();
            return options;
        }
    }
}
