﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Metrics;
using Metrics.Core;

namespace Owin.Metrics.Middleware
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class RequestTimerMiddleware
    {
        private const string TimerItemsKey = "__Mertics.RequestTimer__";

        private readonly Timer requestTimer;
        private AppFunc next;

        public RequestTimerMiddleware(MetricsRegistry registry, string metricName)
        {
            this.requestTimer = registry.Timer(metricName, Unit.Requests, SamplingType.FavourRecent, TimeUnit.Seconds, TimeUnit.Milliseconds);
        }

        public void Initialize(AppFunc next)
        {
            this.next = next;
        }

        /*public async Task Invoke(IDictionary<string, object> environment)
        {
            environment[TimerItemsKey] = this.requestTimer.NewContext();

            await next(environment);

            var timer = environment[TimerItemsKey];
            using (timer as IDisposable) { }
            environment.Remove(TimerItemsKey);
        }*/
    }
}
