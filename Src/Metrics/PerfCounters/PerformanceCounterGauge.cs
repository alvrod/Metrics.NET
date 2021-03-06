﻿using System;
using System.Diagnostics;
using System.Security.Principal;
using Metrics.Core;

namespace Metrics.PerfCounters
{
    public class PerformanceCounterGauge : GaugeMetric
    {
        private readonly PerformanceCounter performanceCounter;

        public PerformanceCounterGauge(string category, string counter)
            : this(category, counter, instance: null)
        { }

        public PerformanceCounterGauge(string category, string counter, string instance)
        {
            try
            {
                this.performanceCounter = instance == null ?
                    new PerformanceCounter(category, counter, true) :
                    new PerformanceCounter(category, counter, instance, true);
            }
            catch (Exception x)
            {
                if (Metric.Config.ErrorHandler != null)
                {
                    Metric.Config.ErrorHandler(x);
                }
                else
                {
                    Trace.Fail("Error reading performance counter data. The application is currently running as user " + WindowsIdentity.GetCurrent().Name +
                    ". Make sure the user has access to the performance counters. The user needs to be either Admin or belong to Performance Monitor user group." +
                    " You can handle this exception by setting a handler on Metric.ErrorHandler", x.ToString());
                }
            }
        }

        public override double Value { get { return this.performanceCounter != null ? this.performanceCounter.NextValue() : -1; } }
    }
}
