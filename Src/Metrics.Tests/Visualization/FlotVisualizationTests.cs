﻿using FluentAssertions;
using Metrics.Visualization;
using Xunit;

namespace Metrics.Tests.Visualization
{
    public class FlotVisualizationTests
    {
        [Fact]
        public void CanReadAppFromResource()
        {
            FlotWebApp.GetFlotApp().Should().NotBeEmpty();
        }
    }
}
