namespace BigBrother.Core
{
    public class BBMetricEvent : BBEvent
    {
        public BBMetricEvent(string message)
            : base(message)
        {
        }

        public override FlexEventType EventType => FlexEventType.Metric;
    }
}