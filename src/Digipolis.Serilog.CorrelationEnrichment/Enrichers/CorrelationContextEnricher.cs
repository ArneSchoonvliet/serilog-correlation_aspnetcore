﻿using System;
using Digipolis.Correlation;
using Digipolis.Serilog.CorrelationEnrichment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;

namespace Digipolis.Serilog
{
    public class CorrelationContextEnricher : ILogEventEnricher
    {
        public CorrelationContextEnricher(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private readonly IHttpContextAccessor _accessor;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = _accessor.HttpContext;
            var ctx = httpContext?.RequestServices?.GetRequiredService<ICorrelationContext>();
            if ( ctx == null ) return;

            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationId, ctx.Id ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationSourceId, ctx.SourceId ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationSourceName, ctx.SourceName ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationInstanceId, ctx.InstanceId ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationInstanceName, ctx.InstanceName ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationHostName, ctx.IpAddress ?? CorrelationLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationLoggingProperties.CorrelationUserName, ctx.UserId ?? CorrelationLoggingProperties.NullValue));
        }
    }
}
