namespace BigBrother.Core
{
    using System;
    using Microsoft.ApplicationInsights.Channel;

    [Obsolete("This isn't ready to be used yet.")]
    public sealed class BBMetricEvent : BBEvent
    {
        public BBMetricEvent(string message)
            : base(message)
        {
        }

        public override FlexEventType EventType => FlexEventType.Metric;

        public override ITelemetry ToTelemetry()
        {
            throw new System.NotImplementedException();
        }
    }
}