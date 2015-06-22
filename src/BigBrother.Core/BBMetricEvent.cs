namespace BigBrother.Core
{
    using Microsoft.ApplicationInsights.Channel;

    public class BBMetricEvent : BBEvent
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